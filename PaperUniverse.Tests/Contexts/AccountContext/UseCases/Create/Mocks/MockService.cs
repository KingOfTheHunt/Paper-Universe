using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Create.Mocks;

public class MockService : IService
{
    public Task SendWelcomeEmail(User user, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}