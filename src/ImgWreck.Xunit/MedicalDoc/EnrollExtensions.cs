using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PortalSvc.Common;
using PortalSvc.Data;

namespace PortalSvc.Core
{
    public static class EnrollExtensions
    {
        public static ResponseEnrollEnum EnrollVerifyInfo(this List<UserProxyView> userproxies,
            Patient patient, EnrollmentInvitation invite)
        {
            if (userproxies.Count > App.Settings.AccountProxyLimit)
            {
                return ResponseEnrollEnum.Enroll_VerifyExceedProxyLimit;
            }

            if (patient == null)
            {
                return ResponseEnrollEnum.Enroll_VerifyInvalidId;
            }

            if (string.Equals(patient.VitalStatus, "deceased", StringComparison.OrdinalIgnoreCase))
            {
                return ResponseEnrollEnum.Enroll_AccountProxyDeactivated;
            }

            if (invite.Source == EnrollInviteSourceEnum.print && userproxies.Any())
            {
                return ResponseEnrollEnum.Enroll_VerifyPatientAlreadyEnrolled;
            }

            if (invite.Source != EnrollInviteSourceEnum.ped
                && invite.Source != EnrollInviteSourceEnum.pes
                && patient.Dob.AddYears(App.Settings.EnrollmentPedOnlyAge) > DateTime.Now)
            {
                return ResponseEnrollEnum.Enroll_VerifyMinorFromNonPed;
            }

            if (!Regex.IsMatch(patient.Mrn, @"^\d{8,8}$")
                && invite.Source != EnrollInviteSourceEnum.pas && invite.Source != EnrollInviteSourceEnum.pes)
            {
                return ResponseEnrollEnum.Enroll_VerifyInvalidMrn;
            }

            return ResponseEnrollEnum.Success;
        }

        [System.Obsolete]
        public static ResponseEnrollEnum EnrollVerifyInfo(this List<UserProxyView> userproxies,
            EnrollVerifyInfoRequest request, Patient patient, EnrollmentInvitation invite)
        {
            var status = userproxies.EnrollVerifyInfo(patient, invite);

            if (status != ResponseEnrollEnum.Success)
            {
                return status;
            }

            if (request.UserProxyRelation == "A" && userproxies.Any(x => x.IsPatient))
            {
                return ResponseEnrollEnum.Enroll_VerifyPatientAlreadyEnrolled;
            }

            var printmatch = invite.Source == EnrollInviteSourceEnum.print && patient.Dob == request.Dob
                    && (request.Phone == patient.PermPhone || request.Phone == patient.PermMobile)
                    && (string.IsNullOrWhiteSpace(request.Mrn) || patient.Mrn.Trim() == request.Mrn.Trim()); // match MRN if given

            var emailmatch = patient.Dob == request.Dob
                && (request.Phone == patient.PermPhone || request.Phone == patient.PermMobile)
                && !string.IsNullOrWhiteSpace(request.Email) && string.Equals(invite.Email?.Trim(), request.Email.Trim(), StringComparison.OrdinalIgnoreCase)
                && (string.IsNullOrWhiteSpace(request.Mrn) || patient.Mrn.Trim() == request.Mrn.Trim()); // match MRN if given

            if (!printmatch && !emailmatch)
            {
                return ResponseEnrollEnum.General_InformationNotMatch;
            }

            return ResponseEnrollEnum.Success;
        }
    }
}
