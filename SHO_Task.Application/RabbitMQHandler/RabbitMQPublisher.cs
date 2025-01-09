using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace SHO_Task.Application;

public class RabbitMQPublisher : IDisposable
{

    private readonly string _queueName = "PurchaseOrderQueue";

    public void Dispose()
    {
        
    }

    public async void Publish<T>(T message)
    {
        var _factory = new ConnectionFactory { HostName = "host.docker.internal" };
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        try
        {
            await channel.QueueDeclareAsync(queue: _queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            string messageBody = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageBody);

            await channel.BasicPublishAsync(exchange: "po_exchange",
                                  routingKey: _queueName,
                                  false,
                                  body: body);

            Console.WriteLine($"Message published to queue {_queueName}: {messageBody}");
        }
        catch (Exception ex) { }
        finally { await channel.CloseAsync(); } 
        
    }
}
