using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Commands;
using ScheduleService.Application.Dto.AvailabilityTemplates.Requests.Queries;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.AddTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.DeleteTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.SetToDefaultCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Command.UpdateTemplateCommand;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetByIdTemplateQuery;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetDefaultTemplateQuery;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.GetUserTemplatesQuery;
using ScheduleService.Application.UseCases.AvailabilityTemplate.Query.IsUserFreeQuery;

namespace ScheduleService.Api.Controllers;

[ApiController]
[Authorize]
[Route("availabilityTemplates")]
public class AvailabilityTemplateController : Controller
{
    private readonly IMediator _mediator;

    public AvailabilityTemplateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddAvailabilityTemplate([FromBody] AddTemplateRequestDto dto,
        CancellationToken cancellationToken)
    {
        var command = new AddTemplateCommand(dto);

        var template = await _mediator.Send(command, cancellationToken);
        return Ok(template);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAvailabilityTemplate([FromBody] UpdateTemplateRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateTemplateCommand(dto);
        
        var updatedTemplate = await _mediator.Send(command, cancellationToken);
        return Ok(updatedTemplate);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAvailabilityTemplate([FromQuery] DeleteTemplateRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new DeleteTemplateCommand(dto);
        
        var updatedTemplate = await _mediator.Send(command, cancellationToken);
        return Ok(updatedTemplate);
    }

    [HttpPatch("default")]
    public async Task<IActionResult> SetToDefaultTemplate([FromQuery] SetToDefaultRequestDto dto,
        CancellationToken cancellationToken)
    {
        var command = new SetToDefaultCommand(dto);

        var updatedTemplate = await _mediator.Send(command, cancellationToken);
        return Ok(updatedTemplate);
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetAvailabilityTemplateById([FromRoute] GetTemplateByIdRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetByIdTemplateQuery(dto);
        
        var updatedTemplate = await _mediator.Send(query, cancellationToken);
        return Ok(updatedTemplate);
    }
    
    [HttpGet("user/{UserId:guid}")]
    public async Task<IActionResult> GetUserAvailabilityTemplates([FromRoute] GetUserTemplatesRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetUserTemplatesQuery(dto);
        
        var templates = await _mediator.Send(query, cancellationToken);
        return Ok(templates);
    }
    
    [HttpGet("user/{UserId:guid}/default")]
    public async Task<IActionResult> GetDefaultTemplate([FromQuery] GetDefaultTemplateRequestDto dto, CancellationToken cancellationToken)
    {
        var query = new GetDefaultTemplateQuery(dto);
        
        var template = await _mediator.Send(query, cancellationToken);
        return Ok(template);
    }
    
    [HttpGet("user/{userId:guid}/free")]
    public async Task<IActionResult> IsUserFree([FromRoute] Guid userId,[FromBody] IsUserFreeRequestDto dto, CancellationToken token)
    {
        var query = new IsUserFreeQuery(userId, dto);
        
        var isFree = await _mediator.Send(query, token);
        return Ok(isFree);
    }
}