using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dal.Context;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _dbContext;

    public TaskRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken)
    {
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return task;
    }

    public async Task<IReadOnlyCollection<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks.AsNoTracking().ToListAsync(cancellationToken);
        return tasks.AsReadOnly();
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> UpdateTitleAsync(Guid id, string? title, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (task == null) return false;

        task.Title = title;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (task == null) return false;

        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Tasks.ExecuteDeleteAsync(cancellationToken);
    }
}