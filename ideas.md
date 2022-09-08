## Streak Complete Stats
- Record the time that the streak was completed on, send a notification in an hours window of the average complete time for that streak using signalR


## SignalR ideas
- When an incoming request is a database command, still return a 202 accepted, but then when the actual database operation is complete, use signalR to send an alert with the specific idea and tell the frontend to fetch/refetch that specific streak so it is in parity with the backend.

## Change mass transit publish to mass transit send so I can recieve a response