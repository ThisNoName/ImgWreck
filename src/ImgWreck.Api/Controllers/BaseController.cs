using Microsoft.AspNetCore.Mvc;

namespace ImgWreck.Api.Controllers;

[Produces("application/json")]
[ApiController]
public class BaseController : ControllerBase
{
    //protected ContextInfo ContextInfo =>
    //    (ContextInfo)HttpContext.Items["contextinfo"] ?? new ContextInfo();
}