using Engine.Core.Model;
using Engine.Exceptions;
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
        entity.CreatedAt = DateTime.Now;
        entity.Version++;
        await DbSet.InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
        _context.AddToTracker(entity);
        return entity;
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => DbSet.DeleteOneAsync(predicate, cancellationToken);

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.DeleteOneAsync(e => e.Id.Equals(entity.Id), cancellationToken);
    }

    public Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default)
        => DbSet.DeleteOneAsync(e => e.Id.Equals(id), cancellationToken);

    public Task DeleteRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken cancellationToken = default)
        => DbSet.DeleteOneAsync(e => entities.Any(i => e.Id.Equals(i.Id)), cancellationToken);

    public void Dispose()
        => _context?.Dispose();

    public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    { 
        var results = await DbSet.Find(predicate).ToListAsync(cancellationToken: cancellationToken)!;
        results.ForEach(_context.AddToTracker);
        return results;
    }

    public async Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var result = await FindOneAsync(e => e.Id.Equals(id), cancellationToken);
        if(result != null)
            _context.AddToTracker(result);
        return result;
    } 

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await DbSet.Find(predicate).SingleOrDefaultAsync(cancellationToken: cancellationToken)!;

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var results = await DbSet.AsQueryable().ToListAsync(cancellationToken);
        results.ForEach(_context.AddToTracker);
        return results;
    } 

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.LastModified = DateTime.Now;
        var providedVersion = entity.Version;
        entity.Version++;
        var result = await DbSet.ReplaceOneAsync(e => e.Id.Equals(entity.Id) && e.Version == providedVersion, entity, new ReplaceOptions(), cancellationToken);
        if (result.ModifiedCount == 0)
            throw new DatabaseException($"The version provided is incorrect, please update your version. Entity type: {typeof(TEntity).Name}. ID: {entity.Id}");
        return entity;
    }
}
