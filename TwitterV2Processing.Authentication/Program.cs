using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TwitterV2Processing.Authentication.Business;
using TwitterV2Processing.Authentication.DbSettings;
using TwitterV2Processing.Authentication.Jwt;
using TwitterV2Processing.Authentication.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("AuthDatabase"));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<JwtTokenHandler>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IUserAccountService, UserAccountService>();
builder.Services.AddHostedService<ConsumerService>();

builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetValue<string>("AuthDatabase:ConnectionString")));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
