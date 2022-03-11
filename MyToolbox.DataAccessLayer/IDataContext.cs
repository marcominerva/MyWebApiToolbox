namespace MyToolbox.DataAccessLayer;

public interface IDataContext : IReadOnlyDataContext
{
    ValueTask<T> GetAsync<T>(params object[] keyValues) where T : class;

    void Insert<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;

    void Delete<T>(IEnumerable<T> entities) where T : class;

    Task SaveAsync();

    Task ExecuteTransactionAsync(Func<Task> action);
}
