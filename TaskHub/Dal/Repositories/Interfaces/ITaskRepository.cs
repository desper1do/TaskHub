using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dal.Entities;

namespace Dal.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TaskEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateTitleAsync(Guid id, string? title, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAllAsync(CancellationToken cancellationToken);
}