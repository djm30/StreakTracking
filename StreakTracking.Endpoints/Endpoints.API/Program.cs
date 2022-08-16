
using MediatR;
using StreakTracking.Endpoints.API.Extensions;
using StreakTracking.Endpoints.Application;
using StreakTracking.Endpoints.Infrastructure.ServiceRegistration;

var CorsPolicy = "CorsPolicy";
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000");
        });
});

// ONLY FOR SEEDING DATABASE
builder.Services.AddDatabaseSeedingService();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(CorsPolicy);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();