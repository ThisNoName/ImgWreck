using ImgWreck.Svc;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImgWreck.Api.Controllers;

public class HomeController : BaseController
{
    private readonly ISender _sender;
    public HomeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/Ping")]
    public async Task<string> Ping()
    {
        return await _sender.Send(new PingRequest());
    }

    // copilot auto generated
    //private readonly IMediator _mediator;
    //public PingController(IMediator mediator)
    //{
    //    _mediator = mediator;
    //}
    //[HttpGet]
    //public async Task<ActionResult<string>> Get()
    //{
    //    return Ok(await _mediator.Send(new PingRequest()));
    //}
}
