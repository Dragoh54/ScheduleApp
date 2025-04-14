using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;

namespace ScheduleService.Api.Controllers;

[ApiController]
[Route("availabilityTemplates")]
public class AvailabilityTemplateController(
    IMediator mediator
    ) : Controller
{

    [HttpPost]
    public async Task<IResult> AddAvailabilityTemplate([FromBody] AddTemplateCommand template, CancellationToken cancellationToken)
    {
        var newTemplate = await mediator.Send(template, cancellationToken);
        return Results.Ok(newTemplate);
    }
}