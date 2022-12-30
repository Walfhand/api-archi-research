using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Engine.Mongo;
public class MongoDbContext : IMongoDbContext
{
    public IClientSessionHandle? Session { get; set; }
    public IMongoDatabase Database { get; }
    public IMongoClient MongoClient { get; }

    protected readonly IList<Func<Task>> _commands;
    public MongoDbContext(IOptions<MongoOptions> options)
    {
        RegisterConventions();
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
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

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        Session = await MongoClient.StartSessionAsync(cancellationToken: cancellationToken);
        Session.StartTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Session is { IsInTransaction: true })
            await Session.CommitTransactionAsync(cancellationToken);

        Session?.Dispose();
    }

    public void Dispose()
    {
        while (Session is { IsInTransaction: true })
            Thread.Sleep(TimeSpan.FromMilliseconds(100));

        GC.SuppressFinalize(this);
    }

    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        return Database.GetCollection<T>(name ?? typeof(T).Name.ToLower());
    }

    public async Task RollbackTransaction(CancellationToken cancellationToken = default)
        => await Session?.AbortTransactionAsync(cancellationToken)!;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
