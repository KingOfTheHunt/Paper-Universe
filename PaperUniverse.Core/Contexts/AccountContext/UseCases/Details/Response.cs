using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Details;

public class Response : SharedContext.UseCases.Response
{
    public ResponseData? Data { get; set; }
    
    public Response(string message, int status, IEnumerable<Notification>? notifications = null) 
        : base(message, status, notifications)
    {
    }

    public Response(string message, int status, ResponseData data, IEnumerable<Notification>? notifications = null)
        : base(message, status, notifications)
    {
        Data = data;
    }
}

public record ResponseData
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}