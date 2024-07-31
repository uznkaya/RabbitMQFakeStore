using MassTransit;
using Microsoft.EntityFrameworkCore;
using Publisher.Mapping;
using Shared;
using Shared.RequestResponseMessageModel.Product;
using Shared.RequestResponseMessages;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<ApplicationDbContext>();

string rabbitMqUri = "amqp://guest:guest@localhost:5672";
string requestQueueName = "FakeStoreQueue";

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(rabbitMqUri), h => { });
    });

    x.AddRequestClient<Product>(new Uri($"{rabbitMqUri}/{requestQueueName}"));
});

builder.Services.AddMassTransitHostedService();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

