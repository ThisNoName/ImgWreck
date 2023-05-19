using ImgWreck.Common;
using MediatR;

namespace ImgWreck.Svc;

public class PingRequest : IRequest<string> { }

public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"{App.Settings.AppName} {App.Settings.AppEnv} running on {Environment.MachineName}");
    }
}

// copilot auto generated
//public class PingHandler : IRequestHandler<PingRequest, string>
//{
//    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
//    {
//        return Task.FromResult("Pong");
//    }
//}