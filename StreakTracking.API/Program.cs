using MassTransit;
using StreakTracking.API.EventHandler.Extensions;
using StreakTracking.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x => { x.UsingRabbitMq(); });
builder.Services.AddMassTransitHostedService();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddInfrastructureServices();
builder.Services.AddScoped<IStreakReadingService, StreakReadingService>();


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