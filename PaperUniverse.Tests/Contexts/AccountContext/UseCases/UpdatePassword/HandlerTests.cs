using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.UpdatePassword.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.UpdatePassword;

[TestClass]
public class HandlerTests
{
    private readonly IRepository _repository;

    public HandlerTests()
    {
        _repository = new MockRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenThePasswordAreDifferent()
    {
        var request = new Request
        {
            Email = "john.doe@gmail.com",
            NewPassword = "password123",
            NewPasswordAgain = "password12"
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, CancellationToken.None);

        var message = result.Notifications
            .FirstOrDefault(x => x.Message.Contains("As senhas não são iguais."));
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("As senhas não são iguais.", message.Message);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenTheRequestIsInvalid()
    {
        var request = new Request
        {
            Email = "",
            NewPassword = "",
            NewPasswordAgain = "",
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnTrueWhenTheRequestIsValid()
    {
        var request = new Request
        {
            Email = "john.doe@gmail.com",
            NewPassword = "password123",
            NewPasswordAgain = "password123"
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.IsTrue(result.Success);
    }
}