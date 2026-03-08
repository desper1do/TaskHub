using Api.DiTest.Extensions;
using Api.DiTest.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api;

/// <summary>
/// Точка входа приложения
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Запуск приложения
    /// </summary>
    public static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();

        var logger = host.Services.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("====== START TEST DI ======");

        using (var scope1 = host.Services.CreateScope())
        {
            logger.LogInformation("--- SCOPE 1 START ---");

            var provider = scope1.ServiceProvider;

            provider.ResolveAndCompare<ISingleton1>();
            provider.ResolveAndCompare<ISingleton2>();

            provider.ResolveAndCompare<IScoped1>();
            provider.ResolveAndCompare<IScoped2>();

            provider.ResolveAndCompare<ITransient1>();
            provider.ResolveAndCompare<ITransient2>();

            logger.LogInformation("--- SCOPE 1 END ---");
        }

        using (var scope2 = host.Services.CreateScope())
        {
            logger.LogInformation("--- SCOPE 2 START ---");

            var provider = scope2.ServiceProvider;

            provider.ResolveAndCompare<ISingleton1>();
            provider.ResolveAndCompare<ISingleton2>();

            provider.ResolveAndCompare<IScoped1>();
            provider.ResolveAndCompare<IScoped2>();

            provider.ResolveAndCompare<ITransient1>();
            provider.ResolveAndCompare<ITransient2>();

            logger.LogInformation("--- SCOPE 2 END ---");
        }

        logger.LogInformation("====== END TEST DI ======");
    }
}