namespace API_User.Repositories
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } = string.Empty;
    }
}
