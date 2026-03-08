using Microsoft.Extensions.Logging;
using Api.DiTest.Interfaces;

namespace Api.DiTest.Services;

public class Singleton1 : DisposedService, ISingleton1
{
    public Singleton1(ILogger<Singleton1> logger) : base(logger) { }
}

public class Singleton2 : DisposedService, ISingleton2
{
    public Singleton2(ILogger<Singleton2> logger) : base(logger) { }
}

public class Scoped1 : DisposedService, IScoped1
{
    public Scoped1(ILogger<Scoped1> logger) : base(logger) { }
}

public class Scoped2 : DisposedService, IScoped2
{
    public Scoped2(ILogger<Scoped2> logger) : base(logger) { }
}

public class Transient1 : DisposedService, ITransient1
{
    public Transient1(ILogger<Transient1> logger) : base(logger) { }
}

public class Transient2 : DisposedService, ITransient2
{
    public Transient2(ILogger<Transient2> logger) : base(logger) { }
}