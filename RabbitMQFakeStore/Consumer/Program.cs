using Consumer.Consumers;
using Consumer.ExternalServices;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Publisher.Mapping;
using Shared;
using Shared.RequestResponseMessages;

var services = new ServiceCollection();

services.AddDbContext<ApplicationDbContext>();
services.AddScoped<CreateProductConsumer>();
services.AddHttpClient<FakeStoreService>();
services.AddAutoMapper(typeof(MapProfile));


//not: RequestMessageConsumer sınıfınıza DbContext bağımlılığını enjekte edebilirsiniz. Bu sayede, consumer'ınız DbContext'i kullanarak veritabanı işlemlerini gerçekleştirebilir.


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

var provider = services.BuildServiceProvider();

var busControl = provider.GetRequiredService<IBusControl>();
await busControl.StartAsync();

Console.ReadLine();
