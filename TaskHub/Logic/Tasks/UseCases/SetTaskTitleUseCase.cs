using System;
using System.Threading;
using System.Threading.Tasks;
using Dal.Repositories.Interfaces;

namespace Logic.Tasks.UseCases;

public sealed class SetTaskTitleUseCase
{
    private readonly ITaskRepository _repository;

    public SetTaskTitleUseCase(ITaskRepository repository) => _repository = repository;

    public Task<bool> ExecuteAsync(Guid id, string? title, CancellationToken cancellationToken) =>
        _repository.UpdateTitleAsync(id, title, cancellationToken);
}