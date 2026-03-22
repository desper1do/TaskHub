using System.Threading;
using System.Threading.Tasks;
using Dal.Repositories.Interfaces;

namespace Logic.Tasks.UseCases;

public class DeleteTasksUseCase
{
    private readonly ITaskRepository _repository;
    public DeleteTasksUseCase(ITaskRepository repository) => _repository = repository;

    public Task ExecuteAsync(CancellationToken cancellationToken) =>
        _repository.DeleteAllAsync(cancellationToken);
}