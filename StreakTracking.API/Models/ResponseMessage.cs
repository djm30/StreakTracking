using System.Net;
using System.Text.Json.Serialization;

namespace StreakTracking.API.Models;


public class ResponseMessage
{
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}