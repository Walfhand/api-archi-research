using Engine.Core.Model;

namespace Engine.Mongo;
public interface IMongoRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IAggregateRoot<TId>
{
}
