using Newtonsoft.Json;

namespace StreakTracking.Application.Models;

public class ResponseMessageContent<T> : ResponseMessage 
{
    [JsonIgnore]
    public T Content { get; set; }
}