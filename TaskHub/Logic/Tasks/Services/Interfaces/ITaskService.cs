using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Logic.Tasks.Models;

namespace Logic.Tasks.Services.Interfaces;

public interface ITaskService
{
    Task<TaskModel> CreateAsync(Guid userId, string? title, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TaskModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaskModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateTitleAsync(Guid id, string? title, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAllAsync(CancellationToken cancellationToken);
}