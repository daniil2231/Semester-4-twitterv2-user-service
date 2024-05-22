namespace TwitterV2Processing.Authentication.DbSettings
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public string UsersCollectionName { get; set; }
    }
}
