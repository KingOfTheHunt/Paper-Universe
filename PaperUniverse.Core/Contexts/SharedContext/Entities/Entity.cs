using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.SharedContext.Entities;

public abstract class Entity : Notifiable<Notification>
{
    public long Id { get; set; }
}