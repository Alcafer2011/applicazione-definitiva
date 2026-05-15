using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using MongoDB.Driver;
using Message.Service.Models;

namespace Message.Service.Core;

public class MessageBus
{
    private readonly ConnectionFactory _factory;
    private readonly IMongoCollection<BusMessage> _log;

    public MessageBus()
    {
        _factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest"
        };

        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-messaging");
        _log = db.GetCollection<BusMessage>("messages");
    }

    public async Task Publish(BusMessage msg)
    {
        using var conn = _factory.CreateConnection();
        using var ch = conn.CreateModel();

        ch.ExchangeDeclare(exchange: "bos-exchange", type: "topic", durable: true);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msg));
        ch.BasicPublish("bos-exchange", msg.Topic, null, body);

        await _log.InsertOneAsync(msg);
    }
}