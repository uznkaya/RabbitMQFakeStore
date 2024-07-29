/*
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
*/

using Consumer.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Publisher.Mapping;
using Shared;
using Shared.RequestResponseMessages;

var services = new ServiceCollection();
//services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=localhost,1433; Database=RabbitMQFakeStoreDb; User Id=SA; Password=reallyStrongPwd123;TrustServerCertificate=True;MultiSubnetFailover=True"));
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RabbitMQFakeStoreDb;Trusted_Connection=True;MultiSubnetFailover=True"));

//not: RequestMessageConsumer sınıfınıza DbContext bağımlılığını enjekte edebilirsiniz. Bu sayede, consumer'ınız DbContext'i kullanarak veritabanı işlemlerini gerçekleştirebilir.
services.AddScoped<CreateProductConsumer>();

services.AddMassTransit(x =>
{
    x.AddConsumer<CreateProductConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");

        cfg.ReceiveEndpoint("FakeStoreQueue", e =>
        {
            e.ConfigureConsumer<CreateProductConsumer>(context);
        });
    });
});

services.AddAutoMapper(typeof(MapProfile));

var provider = services.BuildServiceProvider();

var busControl = provider.GetRequiredService<IBusControl>();
await busControl.StartAsync();

Console.ReadLine();
