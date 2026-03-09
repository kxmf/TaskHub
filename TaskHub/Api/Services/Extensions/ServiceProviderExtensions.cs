using Microsoft.Extensions.DependencyInjection;
using Api.Services.Interfaces;

namespace Api.Services.Extensions;

public static class ServiceProviderExtensions
{
    public static void ResolveAndLog<TService>(this IServiceProvider provider) 
        where TService : IHasInstanceId
    {
        var first = provider.GetRequiredService<TService>();
        var second = provider.GetRequiredService<TService>();

        Console.WriteLine($" - Service: {typeof(TService).Name}");
        Console.WriteLine($" - First ID:  {first.InstanceId}");
        Console.WriteLine($" - Second ID: {second.InstanceId}");
        Console.WriteLine($" - Is Same:   {ReferenceEquals(first, second)}");
    }
}