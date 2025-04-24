using Microsoft.AspNetCore.Mvc;

namespace MeetingService.Api.Controlles;

[ApiController]
[Route("participants")]
public class ParticipantController : Controller
{
    [HttpGet]
    public async Task<IResult> Health()
    {
        return Results.Ok("Healthy");
    }
}