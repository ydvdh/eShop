using EventBus.Message.Common;
using MassTransit;
using Ordering.API.Data;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

//Application Services
builder.Services.AddApplicationServices();

//Infra services
builder.Services.AddInfraServices(builder.Configuration);

//Mass Transit - RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<EventBusRabbitMQConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        //provide the queue name with consumer settings
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<EventBusRabbitMQConsumer>(ctx);
        });
    });
});

var app = builder.Build();

//Migration
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderDataSeed>>();
    OrderDataSeed.SeedAsync(context, logger).Wait();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Enable swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
