# Streak Tracker
This is to be a habit tracking API that allows you to keep tabs on various habits through the use of streaks. The implementation is not entirely practical, as this is mainly to test Dapper, RabbitMQ and Mass Transit which is overkill for a simple solution like this.
<br/>
<br/>
Notifications should also be able to get pushed via the use of a telegram bot in the future, to test out cross language message parsing.

<br/>
If you would like to use the code, you need to create your own `appsettings.json` 


```
{
  "ConnectionString": "Server={host};Port={port};Database={databaseName};User Id={userName};Password={password}",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

This file needs to be placed into the following folders:
 - StreakTracking.API
 - StreakTracking.EventHandler

It is also worth noting you need to have your own Postgresql instance running somewhere.

