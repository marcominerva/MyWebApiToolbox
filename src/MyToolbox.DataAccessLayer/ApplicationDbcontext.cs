using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyToolbox.DataAccessLayer;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public Task ExecuteTransactionAsync(Func<IDbContextTransaction, Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        var strategy = Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(async () =>
        {
            using var transaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);
            await action.Invoke(transaction).ConfigureAwait(false);
        });
    }

    public Task<T> ExecuteTransactionAsync<T>(Func<IDbContextTransaction, Task<T>> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        var strategy = Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(async () =>
        {
            using var transaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);
            var result = await action.Invoke(transaction).ConfigureAwait(false);

            return result;
        });
    }
}
