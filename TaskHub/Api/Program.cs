using Api.Services.Extensions;
using Api.Services.Interfaces;
using LoggingLibrary;
using Microsoft.Extensions.DependencyInjection; // Не забудьте этот using для CreateScope

namespace Api;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        Console.WriteLine("=== scope 1 start ===");
        using (var scope1 = host.Services.CreateScope())
        {
            RunTest(scope1.ServiceProvider);
            Console.WriteLine("=== scope 1 end ===");
        }

        Console.WriteLine("=== scope 2 start ===");
        using (var scope2 = host.Services.CreateScope())
        {
            RunTest(scope2.ServiceProvider);
            Console.WriteLine("=== scope 2 end ===");
        }

        Console.WriteLine("=== host dispose ===");
        host.Dispose(); 

        Console.ReadKey();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    private static void RunTest(IServiceProvider provider)
    {
        provider.ResolveAndLog<ISingleton1>();
        provider.ResolveAndLog<ISingleton2>();
        provider.ResolveAndLog<IScoped1>();
        provider.ResolveAndLog<IScoped2>();
        provider.ResolveAndLog<ITransient1>();
        provider.ResolveAndLog<ITransient2>();
    }
}