using MongoDB.Driver;

namespace Engine.Mongo;
public class MongoDbContext : IMongoDbContext
{
    public void AddCommand(Func<Task> func)
    {
        throw new NotImplementedException();
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        throw new NotImplementedException();
    }

    public Task RollbackTransaction(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
