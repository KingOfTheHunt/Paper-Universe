using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword;

public class Response : SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null) 
        : base(message, status, notifications)
    {
    }
}