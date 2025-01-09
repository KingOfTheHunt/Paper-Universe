using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;

public interface IService
{
    Task SendEmailAsync(User user);
}