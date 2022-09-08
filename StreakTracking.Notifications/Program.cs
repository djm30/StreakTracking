using System.Reflection;
using MassTransit;
using MediatR;
using StreakTracking.Notifications.Consumers;
using StreakTracking.Notifications.Hubs;
using StreakTracking.Notifications.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<NotificationEventConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("EventBusConnection"));
        cfg.ReceiveEndpoint("notifications-queue", c =>
        {
            c.ConfigureConsumer<NotificationEventConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddSingleton<INotificationService, NotificationService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapHub<NotificationHub>("/notification");

app.Run();