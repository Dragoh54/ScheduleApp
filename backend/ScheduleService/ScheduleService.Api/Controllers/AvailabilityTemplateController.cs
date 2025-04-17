using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.DeleteTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;

namespace ScheduleService.Api.Controllers;

[ApiController]
[Route("availabilityTemplates")]
public class AvailabilityTemplateController(
    IMediator mediator
    ) : Controller
{
    [HttpPost]
    public async Task<IResult> AddAvailabilityTemplate([FromBody] AddTemplateCommand command, CancellationToken cancellationToken)
    {
        var newTemplate = await mediator.Send(command, cancellationToken);
        return Results.Ok(newTemplate);
    }
    
    [HttpPut]
    public async Task<IResult> UpdateAvailabilityTemplate([FromBody] UpdateTemplateCommand command, CancellationToken cancellationToken)
    {
        var updatedTemplate = await mediator.Send(command, cancellationToken);
        return Results.Ok(updatedTemplate);
    }

    [HttpDelete]
    public async Task<IResult> DeleteAvailabilityTemplate([FromQuery] DeleteTemplateCommand command, CancellationToken cancellationToken)
    {
        var updatedTemplate = await mediator.Send(command, cancellationToken);
        return Results.Ok(updatedTemplate);
    }

    //TODO: REFACTOR THIS
    [HttpPatch("default")]
    public async Task<IResult> SetToDefaultTemplate([FromQuery] SetToDefaultCommand command, CancellationToken cancellationToken)
    {
        var updatedTemplate = await mediator.Send(command, cancellationToken);
        return Results.Ok(updatedTemplate);
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetAvailabilityTemplate([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetByIdTemplateQuery { Id = id };
        var updatedTemplate = await mediator.Send(query, cancellationToken);
        return Results.Ok(updatedTemplate);
    }

    [HttpGet("me")]
    public async Task<IResult> GetUserAvailabilityTemplates([FromQuery] GetUserTemplatesQuery query, CancellationToken cancellationToken)
    {
        var templates = await mediator.Send(query, cancellationToken);
        return Results.Ok(templates);
    }
    
    [HttpGet("default")]
    public async Task<IResult> GetDefaultTemplate([FromQuery] GetDefaultTemplateQuery query, CancellationToken cancellationToken)
    {
        var template = await mediator.Send(query, cancellationToken);
        return Results.Ok(template);
    }
}