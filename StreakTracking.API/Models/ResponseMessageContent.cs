namespace StreakTracking.API.Models;

public class ResponseMessageContent<T> : ResponseMessage 
{
    public T Content { get; set; }
}