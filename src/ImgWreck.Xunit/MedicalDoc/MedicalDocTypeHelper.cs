namespace PortalSvc.Data
{
    public static class MedicalDocTypeHelper
    {
        public static bool Is<T>(this List<MedicalDocEntry> entries)
        {
            if (entries == null || entries.Count == 0)
                return false;

            return entries.First().GetType() == typeof(T);
        }

        public static List<T> Get<T>(this List<MedicalDocEntry> entries)
        {
            return entries.OfType<T>().ToList();
        }
    }
}
