using Engine.Core.Event;
using Engine.Core.Model;
using MongoDB.Driver;

namespace Engine.Mongo;
public interface IMongoDbContext : IDisposable
{
    IMongoCollection<T> GetCollection<T>(string? name = null);
    void AddToTracker(IAggregateRoot aggregateRoot);
    IEnumerable<IDomainEvent> GetDomainEvents();
    void AddCommand(Func<Task> func);
}
