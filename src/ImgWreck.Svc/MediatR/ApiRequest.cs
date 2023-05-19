using MediatR;
using System.Text.Json.Serialization;

namespace ImgWreck.Svc;

public abstract class ApiRequest<TResponse> : IRequest<TResponse>
{
    // User identity info from OAuth2 token
    [JsonIgnore]
    public virtual string? UserId { get; set; }
    [JsonIgnore]
    public virtual string? Mrn { get; set; }
    [JsonIgnore]
    public virtual string? Provider { get; set; }
    [JsonIgnore]
    public virtual string? ClientId { get; set; }
    [JsonIgnore]
    public virtual string? CallerInfo { get; set; }
}

// copliot auto generated
// Generic request class for MediatR
//public class ApiRequest<TResponse> : IRequest<TResponse>
//{
//    public ApiRequest()
//    {
//        // set default values
//        this.RequestId = Guid.NewGuid();
//        this.RequestDate = DateTime.UtcNow;
//    }
//    public Guid RequestId { get; set; }
//    public DateTime RequestDate { get; set; }
//}
