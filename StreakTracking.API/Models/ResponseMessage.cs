using System.Net;

namespace StreakTracking.API.Models;


public class ResponseMessage
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}