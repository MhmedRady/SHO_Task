using Microsoft.EntityFrameworkCore.Metadata;
using SHO_Task.Domain.BuildingBlocks;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace SHO_Task.Infrastructure.RabbitMq
{
    public sealed class RabbitMqPublisher : IRabbitMqPublisher, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqPublisher(string rabbitMqConnectionString)
        {
            var amqpUri = new Uri(rabbitMqConnectionString);
            var endpoint = AmqpTcpEndpoint.Parse(amqpUri.OriginalString);

/*            var factory = new ConnectionFactory
            {
                HostName = endpoint.HostName,
                Port = endpoint.Port,
                UserName = amqpUri.UserInfo.Split(':')[0],
                Password = amqpUri.UserInfo.Split(':')[1],
                // Configure other options as needed
            };

            // Create the connection
            _connection = factory.CreateConnectionAsync().Result;*/

            // Create the channel (previously CreateModel() in older versions)
            //_channel = _connection.CreateChannelAsync().Result;
        }
        public async void Publish<T>(T message, string exchangeName, string routingKey)
        {
            // Convert message object to JSON
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            // Declare an exchange (topic, direct, or fanout). Make sure 'durable' is true if needed.
            await _channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Topic, durable: true);

            // Publish the message
           await _channel.BasicPublishAsync(exchangeName, routingKey, body, default);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
