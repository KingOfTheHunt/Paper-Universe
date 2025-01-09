using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Details.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Details.Mocks;

public class MockRepository : IRepository
{
    public async Task<User?> GetUserAsync(string email)
    {
        var name = new Name("John", "Doe");
        var userEmail = new Email("john.doe@gmail.com");
        var password = new Password("12345678");
        var user = new User(name, userEmail, password);

        if (user.Email.Address == email)
            return user;

        return null;
    }
}