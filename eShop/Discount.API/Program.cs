
using Discount.API.Extensions;
using Discount.API.Handlers;
using Discount.API.Repositories;
using Discount.API.Repositories.Interfaces;
using Discount.API.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//Mediatr
var assemblies = new Assembly[]
{

   Assembly.GetExecutingAssembly(), typeof(CreateDiscountHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

//Migrate the Database
app.MigrateDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapGrpcService<DiscountService>();
});

app.Run();
