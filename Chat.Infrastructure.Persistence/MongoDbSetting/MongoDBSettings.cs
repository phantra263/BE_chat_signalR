namespace Chat.Infrastructure.Persistence.MongoDBSetting
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }

    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
