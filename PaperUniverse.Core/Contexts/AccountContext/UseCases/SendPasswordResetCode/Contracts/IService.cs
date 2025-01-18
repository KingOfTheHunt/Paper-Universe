using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts;

public interface IService
{
    Task SendEmailAsync(User user, CancellationToken cancellationToken);
}