using System;
using System.Threading;
using System.Threading.Tasks;
using Dal.Repositories.Interfaces;
using Logic.Tasks.Models;

namespace Logic.Tasks.UseCases;

public class GetTaskUseCase
{
    private readonly ITaskRepository _repository;
    public GetTaskUseCase(ITaskRepository repository) => _repository = repository;

    public async Task<TaskModel?> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return null;

        return new TaskModel
        {
            Id = entity.Id,
            Title = entity.Title,
            CreatedByUserId = entity.CreatedByUserId,
            CreatedUtc = entity.CreatedUtc
        };
    }
}