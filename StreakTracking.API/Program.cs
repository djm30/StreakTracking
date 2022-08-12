
using MediatR;
using StreakTracking.API.Extensions;
using StreakTracking.Application;
using StreakTracking.Application.Helpers;
using StreakTracking.Infrastructure.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Extension methods
builder.Services.ConfigureMassTransit();
builder.Services.AddServices();

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();


// ONLY FOR SEEDING DATABASE
builder.Services.AddDatabaseSeedingService();



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