using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName;

public class Response : SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null) 
        : base(message, status, notifications)
    {
    }
}