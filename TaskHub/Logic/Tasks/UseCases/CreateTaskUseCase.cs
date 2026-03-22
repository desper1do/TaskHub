using System;
using System.Threading;
using System.Threading.Tasks;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Logic.Tasks.Models;

namespace Logic.Tasks.UseCases;

public class CreateTaskUseCase
{
    private readonly ITaskRepository _repository;

    public CreateTaskUseCase(ITaskRepository repository) => _repository = repository;

    public async Task<TaskModel> ExecuteAsync(Guid userId, string? title, CancellationToken cancellationToken)
    {
        var entity = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreatedByUserId = userId,
            CreatedUtc = DateTimeOffset.UtcNow
        };

        var created = await _repository.CreateAsync(entity, cancellationToken);

        return new TaskModel
        {
            Id = created.Id,
            Title = created.Title,
            CreatedByUserId = created.CreatedByUserId,
            CreatedUtc = created.CreatedUtc
        };
    }
}