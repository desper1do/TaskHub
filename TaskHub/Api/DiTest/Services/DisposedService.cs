using System;
using Microsoft.Extensions.Logging;
using Api.DiTest.Interfaces;

namespace Api.DiTest.Services;

public abstract class DisposedService : IDisposable, IHasInstanceId
{
    private readonly ILogger _logger;

    public Guid InstanceId { get; } = Guid.NewGuid();

    protected DisposedService(ILogger logger)
    {
        _logger = logger;

        _logger.LogInformation(
            "[CREATE] {ServiceName} | InstanceId: {InstanceId}",
            GetType().Name,
            InstanceId);
    }

    public void Dispose()
    {
        _logger.LogInformation(
            "[DISPOSE] {ServiceName} | InstanceId: {InstanceId}",
            GetType().Name,
            InstanceId);
    }
}