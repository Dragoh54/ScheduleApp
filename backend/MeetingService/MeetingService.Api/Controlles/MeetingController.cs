using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controlles;

[ApiController]
[Route("meetings")]
public class MeetingController : Controller
{
    [HttpGet]
    public async Task<IResult> Health()
    {
        return Results.Ok("Healthy");
    }
}