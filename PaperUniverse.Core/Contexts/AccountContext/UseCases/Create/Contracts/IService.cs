using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendWelcomeEmail(User user, CancellationToken cancellationToken);
}