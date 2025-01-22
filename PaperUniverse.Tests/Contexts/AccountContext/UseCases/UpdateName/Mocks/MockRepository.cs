using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.UpdateName.Mocks;

public class MockRepository : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var name = new Name("John", "Smith");
        var userEmail = new Email("john.smith@gmail.com");
        var password = new Password("123456789");
        var user = new User(name, userEmail, password);
        
        if (user.Email.Address == email)
            return user;

        return null;
    }

    public Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}