using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Api.DiTest.Interfaces;

namespace Api.DiTest.Extensions;

public static class ServiceProviderExtensions
{
    public static void ResolveAndCompare<TService>(this IServiceProvider provider)
        where TService : IHasInstanceId
    {
        var logger = provider.GetRequiredService<ILogger<TService>>();

        var first = provider.GetRequiredService<TService>();
        var second = provider.GetRequiredService<TService>();

        logger.LogInformation("Service: {ServiceType}", typeof(TService).Name);
        logger.LogInformation("First InstanceId: {FirstId}", first.InstanceId);
        logger.LogInformation("Second InstanceId: {SecondId}", second.InstanceId);

        logger.LogInformation(
            "Same instance: {IsSame}",
            ReferenceEquals(first, second));

        logger.LogInformation("-----------------------------------");
    }
}