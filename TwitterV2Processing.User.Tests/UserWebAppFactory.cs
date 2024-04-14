using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using TwitterV2Processing.User.Persistence;

namespace TwitterV2Processing.User.Tests
{
    internal class UserWebAppFactory : WebApplicationFactory<Program>
    {
        private MongoDbRunner runner;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IUserRepository));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                runner = MongoDbRunner.Start();

                services.AddSingleton<IMongoClient>(new MongoClient(runner.ConnectionString));

                services.AddSingleton<IUserRepository, UserRepository>();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                runner.Dispose();
            }
        }
    }
}
