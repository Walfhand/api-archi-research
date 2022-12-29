namespace Engine.Mongo;
public class MongoOptions
{
    public const string Mongo = "Mongo";
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}
