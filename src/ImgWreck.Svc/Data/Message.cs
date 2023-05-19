using Insight.Database;

namespace ImgWreck.Svc;

public class Message
{
    [Column("Msg_Id")]
    public int Id { get; set; }
    public string ToId { get; set; } = string.Empty;
    public string FromId { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
