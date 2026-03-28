using InventoryService.Data;
using InventoryService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddHostedService<MessageBusWorker>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddGrpc();

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("InventoryConn")));
else
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMem"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

PrepDb.PrepPopulation(app, builder.Environment.IsProduction());
app.MapGrpcService<GrpcInventoryService>();
app.MapControllers();
app.Run();