using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}