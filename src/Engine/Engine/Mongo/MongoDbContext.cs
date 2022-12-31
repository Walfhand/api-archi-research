using Engine.Core.Event;
using Engine.Core.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Engine.Mongo;
public class MongoDbContext : IMongoDbContext
{
    public IClientSessionHandle? Session { get; set; }
    public IMongoDatabase Database { get; }
    public IMongoClient MongoClient { get; }
    private readonly List<IAggregateRoot> _changeTracker = new();

    protected readonly IList<Func<Task>> _commands;
    public MongoDbContext(IOptions<MongoOptions> options)
    {
        RegisterConventions();
        MongoClient = new MongoClient(options.Value.ConnectionString);

        Database = MongoClient.GetDatabase(options.Value.DatabaseName);
        _commands = new List<Func<Task>>();
    }

    private static void RegisterConventions()
    {
        ConventionRegistry.Register(
            "conventions",
            new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new IgnoreIfDefaultConvention(false)
            }, _ => true);
    }
    public void AddCommand(Func<Task> func)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        while (Session is { IsInTransaction: true })
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        _changeTracker.Clear();
        GC.SuppressFinalize(this);
    }

    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        return Database.GetCollection<T>(name ?? typeof(T).Name.ToLower());
    }

    public void AddToTracker(IAggregateRoot aggregateRoot)
    {
        _changeTracker.Add(aggregateRoot);
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
        => _changeTracker.Where(x => x.DomainEvents.Any()).SelectMany(x => x.DomainEvents);
}
