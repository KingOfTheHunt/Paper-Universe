using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Details.Contracts;

public interface IRepository
{
    Task<User?> GetUserAsync(string email);
}