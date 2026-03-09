using Api.Services.Interfaces;

namespace Api.Services;

public abstract class DisposedService : IHasInstanceId, IDisposable
{
    public Guid InstanceId { get; } = Guid.NewGuid();
    protected DisposedService() => Console.WriteLine($"Created {GetType().Name} with ID {InstanceId}");
    public void Dispose() => Console.WriteLine($"Disposed {GetType().Name} with ID {InstanceId}");
}