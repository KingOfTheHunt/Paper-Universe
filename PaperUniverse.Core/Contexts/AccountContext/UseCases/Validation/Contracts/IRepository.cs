using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Validation.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task SaveAsync(User user);
}