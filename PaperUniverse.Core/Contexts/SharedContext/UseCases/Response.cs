using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.SharedContext.UseCases;

public abstract class Response
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; } = 200;
    public IEnumerable<Notification>? Notifications { get; set; }
    public bool Success => Status is >= 200 and <= 299;
    
    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }
}