using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.ResendVerification.Mocks;

public class MockService : IService
{
    public Task SendEmailAsync(User user)
    {
        return Task.CompletedTask;
    }
}