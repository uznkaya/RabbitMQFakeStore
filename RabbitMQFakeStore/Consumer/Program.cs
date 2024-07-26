
using Consumer.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.RequestResponseMessages;


var services = new ServiceCollection();
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=localhost,1433; Database=RabbitMQFakeStoreDb; User Id=SA; Password=reallyStrongPwd123;TrustServerCertificate=True;MultiSubnetFailover=True"));



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

