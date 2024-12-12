using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Validation.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Validation.Mocks;

public class MockRepository : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var name = new Name("John", "Doe");
        var userEmail = new Email("john.doe@gmail.com");
        var password = new Password("12345678");    
        var user = new User(name, userEmail, password);
        
        if (email == userEmail.Address)
            return Task.FromResult<User?>(user).Result;
        
        return null;
    }

    public Task SaveAsync(User user)
    {
        return Task.CompletedTask;
    }
}