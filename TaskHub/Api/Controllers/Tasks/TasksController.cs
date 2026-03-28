using Api.Attributes;
using Api.Controllers.Tasks.Requests;
using Api.Controllers.Tasks.Responses;
using Api.Filters;
using Logic.Tasks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Tasks;

[ApiController]
[Route("tasks")]
[ServiceFilter(typeof(StudentInfoHeadersFilter))]
[ServiceFilter(typeof(RequestLoggingFilter))]
public sealed class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidateCreateTaskRequestFilter))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateAsync(request.CreatedByUserId, request.Title, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, ToResponse(task));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllAsync(cancellationToken);
        return Ok(tasks.Select(ToResponse));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRouteTaskId] Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetByIdAsync(id, cancellationToken);
        if (task is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(task));
    }

    [HttpPut("{id}/title")]
    [ServiceFilter(typeof(ValidateSetTaskTitleRequestFilter))]
    public async Task<IActionResult> UpdateTitleAsync([FromRouteTaskId] Guid id, [FromBody] SetTaskTitleRequest request, CancellationToken cancellationToken)
    {
        var updated = await _taskService.UpdateTitleAsync(id, request.Title, cancellationToken);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRouteTaskId] Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _taskService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllAsync(cancellationToken);
        return NoContent();
    }

    private static TaskResponse ToResponse(Logic.Tasks.Models.TaskModel task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        CreatedByUserId = task.CreatedByUserId,
        CreatedUtc = task.CreatedUtc
    };
}