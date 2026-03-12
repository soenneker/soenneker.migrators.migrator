using Microsoft.Extensions.Logging;
using Soenneker.Migrators.Migrator.Abstract;

namespace Soenneker.Migrators.Migrator;

///<inheritdoc cref="IMigrator"/>
public abstract class Migrator : IMigrator
{
    protected ILogger<Migrator> Logger { get; }

    protected bool Log => false;

    protected Migrator(ILogger<Migrator> logger)
    {
        Logger = logger;
    }
}