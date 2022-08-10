using System.Net;
using System.Text.Json.Serialization;

namespace StreakTracking.Application.Models;


public class ResponseMessage
{
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}