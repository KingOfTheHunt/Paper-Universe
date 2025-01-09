using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.ResendVerification.Mocks;

public class MockRepository : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var name = new Name("John", "Doe");
        var userEmail = new Email("john.doe@gmail.com");
        var password = new Password("password");
        var user = new User(name, userEmail, password);

        if (email == userEmail.Address)
            return user;
        
        return null;
    }

    public Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}