using Api.Services.Interfaces;

namespace Api.Services;

public class Singleton1 : DisposedService, ISingleton1 { }
public class Singleton2 : DisposedService, ISingleton2 { }
public class Scoped1 : DisposedService, IScoped1 { }
public class Scoped2 : DisposedService, IScoped2 { }
public class Transient1 : DisposedService, ITransient1 { }
public class Transient2 : DisposedService, ITransient2 { }