using System;

namespace Api.DiTest;

// 1. интерфейс для получения ID экземпляра, чтобы видеть, какие экземпляры создаются и уничтожаются
public interface IHasInstanceId
{
    Guid InstanceId { get; }
}

// 2. базовый класс для всех сервисов, который логирует создание и уничтожение экземпляров
public abstract class DisposedService : IDisposable, IHasInstanceId
{
    public Guid InstanceId { get; } = Guid.NewGuid();
    private readonly string _serviceName;

    protected DisposedService()
    {
        _serviceName = GetType().Name;
        Console.WriteLine($"[CREATE] Создан: {_serviceName} | ID: {InstanceId}");
    }

    public void Dispose()
    {
        Console.WriteLine($"[DISPOSE] Уничтожен: {_serviceName} | ID: {InstanceId}");
    }
}

// 3. интерфейсы для разных типов сервисов
public interface ISingleton1 : IHasInstanceId {}
public interface ISingleton2 : IHasInstanceId {}
public interface IScoped1 : IHasInstanceId {}
public interface IScoped2 : IHasInstanceId {}
public interface ITransient1 : IHasInstanceId {}
public interface ITransient2 : IHasInstanceId {}

// 4. реализации сервисов, которые логируют свое создание и уничтожение
public class Singleton1 : DisposedService, ISingleton1 {}
public class Singleton2 : DisposedService, ISingleton2 {}
public class Scoped1 : DisposedService, IScoped1 {}
public class Scoped2 : DisposedService, IScoped2 {}
public class Transient1 : DisposedService, ITransient1 {}
public class Transient2 : DisposedService, ITransient2 {}