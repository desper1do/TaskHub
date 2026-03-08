using System;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DiTest;

public static class ServiceProviderExtensions
{
    public static void ResolveAndCompare<TService>(this IServiceProvider provider) where TService : IHasInstanceId
    {
        // Достаем сервис два раза подряд
        var first = provider.GetRequiredService<TService>();
        var second = provider.GetRequiredService<TService>();

        Console.WriteLine($"Тип: {typeof(TService).Name}");
        Console.WriteLine($"  Первый вызов ID: {first.InstanceId}");
        Console.WriteLine($"  Второй вызов ID: {second.InstanceId}");
        Console.WriteLine($"  Это один и тот же объект? {ReferenceEquals(first, second)}");
        Console.WriteLine(new string('-', 40));
    }
}