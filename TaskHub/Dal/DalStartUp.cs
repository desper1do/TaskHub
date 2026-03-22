using Dal.Context;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using DatabaseLibrary;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

public static class DalStartUp
{
    public static void AddDal(this IServiceCollection services)
    {
        services.AddDatabase<UserDbContext>();
        services.AddDatabase<TaskDbContext>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}