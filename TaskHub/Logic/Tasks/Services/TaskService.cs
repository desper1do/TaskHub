using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Logic.Tasks.Models;
using Logic.Tasks.Services.Interfaces;
using Logic.Tasks.UseCases;

namespace Logic.Tasks.Services;

internal sealed class TaskService : ITaskService
{
    private readonly CreateTaskUseCase _createTaskUseCase;
    private readonly GetTasksUseCase _getTasksUseCase;
    private readonly GetTaskUseCase _getTaskUseCase;
    private readonly SetTaskTitleUseCase _setTaskTitleUseCase;
    private readonly DeleteTaskUseCase _deleteTaskUseCase;
    private readonly DeleteTasksUseCase _deleteTasksUseCase;

    public TaskService(
        CreateTaskUseCase createTaskUseCase,
        GetTasksUseCase getTasksUseCase,
        GetTaskUseCase getTaskUseCase,
        SetTaskTitleUseCase setTaskTitleUseCase,
        DeleteTaskUseCase deleteTaskUseCase,
        DeleteTasksUseCase deleteTasksUseCase)
    {
        _createTaskUseCase = createTaskUseCase;
        _getTasksUseCase = getTasksUseCase;
        _getTaskUseCase = getTaskUseCase;
        _setTaskTitleUseCase = setTaskTitleUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _deleteTasksUseCase = deleteTasksUseCase;
    }

    public Task<TaskModel> CreateAsync(Guid userId, string? title, CancellationToken cancellationToken) =>
        _createTaskUseCase.ExecuteAsync(userId, title, cancellationToken);

    public Task<IReadOnlyCollection<TaskModel>> GetAllAsync(CancellationToken cancellationToken) =>
        _getTasksUseCase.ExecuteAsync(cancellationToken);

    public Task<TaskModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _getTaskUseCase.ExecuteAsync(id, cancellationToken);

    public Task<bool> UpdateTitleAsync(Guid id, string? title, CancellationToken cancellationToken) =>
        _setTaskTitleUseCase.ExecuteAsync(id, title, cancellationToken);

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        _deleteTaskUseCase.ExecuteAsync(id, cancellationToken);

    public Task DeleteAllAsync(CancellationToken cancellationToken) =>
        _deleteTasksUseCase.ExecuteAsync(cancellationToken);
}