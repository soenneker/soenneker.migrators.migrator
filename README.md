# Soenneker.Migrators.Migrator
[![](https://img.shields.io/nuget/v/soenneker.migrators.migrator.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.migrators.migrator/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.migrators.migrator/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.migrators.migrator/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.migrators.migrator.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.migrators.migrator/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.migrators.migrator/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.migrators.migrator/actions/workflows/codeql.yml)

Provides a marker interface and logger-bearing base class for application-defined, one-time migrators.

## Installation

```bash
dotnet add package Soenneker.Migrators.Migrator
```

## Define a migrator

The application defines the execution method and dependencies appropriate to its migration framework:

```csharp
using Microsoft.Extensions.Logging;
using Soenneker.Migrators.Migrator;

public sealed class BackfillCustomerNamesMigrator : Migrator
{
    private readonly ICustomerRepository _customers;

    public BackfillCustomerNamesMigrator(
        ILogger<Migrator> logger,
        ICustomerRepository customers) : base(logger)
    {
        _customers = customers;
    }

    protected override bool Log => true;

    public async Task Run(CancellationToken cancellationToken)
    {
        if (Log)
            Logger.LogInformation("Starting customer-name backfill");

        await _customers.BackfillMissingNames(cancellationToken);
    }
}
```

`Logger` uses the `Soenneker.Migrators.Migrator.Migrator` logging category. `Log` defaults to `false`; it is only a protected switch for code in the derived class and does not cause the base class to emit logs.

## Scope

`IMigrator` is a marker with no execution method. The package does not discover or run migrators, persist completion state, enforce idempotency, open transactions, serialize concurrent instances, retry failures, or provide rollback. The host application must define those guarantees before invoking a migrator.

For destructive or non-repeatable work, record a durable migration identifier and completion state in the same transactional boundary as the data change whenever the backing store supports it. Propagate cancellation and make retry behavior explicit.
