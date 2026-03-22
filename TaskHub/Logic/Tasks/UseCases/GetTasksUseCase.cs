using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dal.Repositories.Interfaces;
using Logic.Tasks.Models;

namespace Logic.Tasks.UseCases;

public sealed class GetTasksUseCase
{
    private readonly ITaskRepository _repository;

    public GetTasksUseCase(ITaskRepository repository) => _repository = repository;

    public async Task<IReadOnlyCollection<TaskModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new TaskModel
        {
            Id = e.Id,
            Title = e.Title,
            CreatedByUserId = e.CreatedByUserId,
            CreatedUtc = e.CreatedUtc
        }).ToList().AsReadOnly();
    }
}