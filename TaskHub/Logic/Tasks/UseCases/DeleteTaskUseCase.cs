using System;
using System.Threading;
using System.Threading.Tasks;
using Dal.Repositories.Interfaces;

namespace Logic.Tasks.UseCases;

public class DeleteTaskUseCase
{
    private readonly ITaskRepository _repository;
    public DeleteTaskUseCase(ITaskRepository repository) => _repository = repository;

    public Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken) =>
        _repository.DeleteAsync(id, cancellationToken);
}