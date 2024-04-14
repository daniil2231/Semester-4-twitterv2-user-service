using Mongo2Go;
using MongoDB.Driver;

namespace TwitterV2Processing.User.Tests
{
    public class MongoFixture : IDisposable
    {
        public MongoDbRunner Runner { get; private set; }
        public MongoClient Client { get; private set; }

        public MongoFixture()
        {
            Runner = MongoDbRunner.Start();
            Client = new MongoClient(Runner.ConnectionString);
        }

        public void Dispose()
        {
            Runner.Dispose();
        }
    }
}
