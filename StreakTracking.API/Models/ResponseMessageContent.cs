using Newtonsoft.Json;

namespace StreakTracking.API.Models;

public class ResponseMessageContent<T> : ResponseMessage 
{
    [JsonIgnore]
    public T Content { get; set; }
}