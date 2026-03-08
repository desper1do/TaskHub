using LoggingLibrary;
using Api.DiTest;

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
        // 1. создаем хост приложения, который будет управлять жизненным циклом сервисов и сервера
        var host = Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();

        // 2. тестируем DI, вручную создавая скоупы и запрашивая сервисы, чтобы увидеть, как они создаются и уничтожаются
        Console.WriteLine("\n====== НАЧАЛО ТЕСТА DI ======\n");

        using (var scope1 = host.Services.CreateScope())
        {
            Console.WriteLine("\n--- SCOPE 1 START ---");
            var p = scope1.ServiceProvider;
            p.ResolveAndCompare<ISingleton1>();
            p.ResolveAndCompare<ISingleton2>();
            p.ResolveAndCompare<IScoped1>();
            p.ResolveAndCompare<IScoped2>();
            p.ResolveAndCompare<ITransient1>();
            p.ResolveAndCompare<ITransient2>();
            Console.WriteLine("--- SCOPE 1 END (Сейчас умрут Scoped и Transient) ---\n");
        } // тут умрут Scoped1, Scoped2, Transient1 и Transient2, а Singleton'ы останутся живы, потому что они синглтоны

        using (var scope2 = host.Services.CreateScope())
        {
            Console.WriteLine("\n--- SCOPE 2 START ---");
            var p = scope2.ServiceProvider;
            p.ResolveAndCompare<ISingleton1>();
            p.ResolveAndCompare<ISingleton2>();
            p.ResolveAndCompare<IScoped1>();
            p.ResolveAndCompare<IScoped2>();
            p.ResolveAndCompare<ITransient1>();
            p.ResolveAndCompare<ITransient2>();
            Console.WriteLine("--- SCOPE 2 END ---\n");
        }

        Console.WriteLine("\n====== ЗАВЕРШАЕМ HOST (Сейчас умрут Singleton) ======\n");
        
        // 3. запускаем хост, который будет держать приложение в рабочем состоянии, обрабатывать запросы и управлять жизненным циклом сервисов
        host.Dispose();
    }
}