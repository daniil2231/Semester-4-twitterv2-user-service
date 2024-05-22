using Confluent.Kafka;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net;
using TwitterV2Processing.Authentication.Models;
using TwitterV2Processing.Authentication.Persistence;

namespace TwitterV2Processing.Authentication.Business
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ILogger<ConsumerService> _logger;
        private readonly ConsumerConfig consumerConfig;
        private readonly IAuthRepository _authRepository;

        public ConsumerService(ILogger<ConsumerService> logger, IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;

            consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "kafka:9092",
                GroupId = "auth_service",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string[] topics = ["user_deletion", "user_creation"];
            _consumer.Subscribe(topics);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(async () => ProcessKafkaMessage(stoppingToken), stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _consumer.Close();
        }

        public async Task ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                var topic = consumeResult.Topic;
                var messageJson = consumeResult.Message.Value;
                var message = JsonConvert.DeserializeObject<dynamic>(messageJson);

                if (topic == "user_deletion")
                {
                    var username = (string)message.Username;

                    await DeleteUserAccount(username);
                }

                if (topic == "user_creation")
                {
                    var username = (string)message.Username;
                    var password = (string)message.Password;
                    var role = (string)message.Role;

                    await CreateUserAccount(username, password, role);
                }

                _logger.LogInformation($"Received inventory update: {message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing Kafka message: {ex.Message}");
            }
        }

        public async Task<DeleteResult> DeleteUserAccount(string username)
        {
            return await _authRepository.DeleteUserAccount(username);
        }

        public async Task CreateUserAccount(string username, string password, string role)
        {
            UserAccount newUser = new UserAccount { Id = "", Username = username, Password = password, Role = role };
            await _authRepository.CreateUserAccount(newUser);
        } 
    }
}
