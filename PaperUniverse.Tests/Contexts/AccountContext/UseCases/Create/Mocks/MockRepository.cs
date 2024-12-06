using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Create.Mocks;

public class MockRepository : IRepository
{
    public Task<bool> AnyEmailAsync(string email, CancellationToken cancellationToken)
    {
        if (email == "test@test.com")
            return Task.FromResult(true);
        
        return Task.FromResult(false);
    }

    public Task SaveAsync(User user, CancellationToken cancellationToken) => 
        Task.CompletedTask;
}