namespace ImgWreck.Svc;

public class ApiResponse
{
    public string Id { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class ApiResponse<TResponse>
{
    public string Id { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public TResponse? Data { get; set; }
}

// copilot auto generated
// Generic ApiResponse class for MediatR
//public class ApiResponse<TResponse>
//{
//    public ApiResponse()
//    {
//        // set default values
//        this.ResponseId = Guid.NewGuid();
//        this.ResponseDate = DateTime.UtcNow;
//    }
//    public Guid ResponseId { get; set; }
//    public DateTime ResponseDate { get; set; }
//    public TResponse? Data { get; set; }
//}