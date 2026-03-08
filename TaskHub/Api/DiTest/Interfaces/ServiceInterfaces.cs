namespace Api.DiTest.Interfaces;

public interface ISingleton1 : IHasInstanceId {}
public interface ISingleton2 : IHasInstanceId {}

public interface IScoped1 : IHasInstanceId {}
public interface IScoped2 : IHasInstanceId {}

public interface ITransient1 : IHasInstanceId {}
public interface ITransient2 : IHasInstanceId {}