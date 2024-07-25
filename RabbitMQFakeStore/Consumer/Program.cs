
using Consumer.Consumers;
using MassTransit;
using Shared.RequestResponseMessages;

string rabbitMqUri = "amqp://guest:guest@localhost:5672";
string requestQueueName = "FakeStoreQueue";

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host(rabbitMqUri);
    cfg.ReceiveEndpoint("FakeStoreQueue", endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();

Console.ReadLine();

