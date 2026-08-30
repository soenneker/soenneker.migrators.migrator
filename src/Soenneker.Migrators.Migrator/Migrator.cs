using Microsoft.Extensions.Logging;
using Soenneker.Migrators.Migrator.Abstract;

namespace Soenneker.Migrators.Migrator;

public abstract class Migrator : IMigrator
{
    /// <summary>
    /// Logger shared with the derived migrator.
    /// </summary>
    protected ILogger<Migrator> Logger { get; }

    /// <summary>
    /// Gets whether the derived migrator has enabled its optional logging paths.
    /// </summary>
    protected virtual bool Log => false;

    protected Migrator(ILogger<Migrator> logger)
    {
        Logger = logger;
    }
}
