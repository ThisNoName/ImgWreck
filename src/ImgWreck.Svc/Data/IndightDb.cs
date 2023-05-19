using Insight.Database;
using System.Data;

namespace ImgWreck.Svc;

public interface IInsightDb : IDbConnection
{
    public IDbConnection GetConnection();

    [Sql("SELECT * FROM Message")]
    public Task<List<Message>> MessageListAsync();

    [Sql("SELECT * FROM Message WHERE Id = @id")]
    public Task<Message> MessageDetailAsync(int id);
}