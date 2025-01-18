using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.SendPasswordResetCode.Mocks;

public class MockService : IService
{
    public Task SendEmailAsync(User user, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}