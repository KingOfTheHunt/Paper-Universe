using PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.Authenticate.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Authenticate;

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
    public async Task ShouldReturnFalseWhenUserNotExists()
    {
        var request = new Request()
        {
            Email = "davi@email.com",
            Password = "password",
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, new CancellationToken());

        Assert.IsFalse(result.Success);
        Assert.AreEqual("Não existe nenhum usuário com esse e-mail.", result.Message);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenPasswordIsInvalid()
    {
        var request = new Request()
        {
            Email = "john.doe@gmail.com",
            Password = "12345678"
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, new CancellationToken());
        
        Assert.IsFalse(result.Success);
    }
}