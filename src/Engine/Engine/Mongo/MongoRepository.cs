using Engine.Core.Model;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Engine.Mongo;
public class MongoRepository<TEntity, TId> : IMongoRepository<TEntity, TId>
     where TEntity : class, IAggregateRoot<TId>
{
    private readonly IMongoDbContext _context;
    protected readonly IMongoCollection<TEntity> DbSet;

    public MongoRepository(IMongoDbContext context)
    {
        _context = context;
        DbSet = _context.GetCollection<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
        return entity;
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TEntity>> RawQuery(string query, CancellationToken cancellationToken = default, params object[] queryParams)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
