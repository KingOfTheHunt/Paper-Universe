using PaperUniverse.Core.Contexts.AccountContext.UseCases.Validation;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Validation.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.Validation.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Validation;

[TestClass]
public class HandlerTests
{
    private IRepository _repository;

    public HandlerTests()
    {
        _repository = new MockRepository();
    }
    
    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenUserIsNotFound()
    {
        var request = new Request
        {
            Email = "test@test.com",
            VerificationCode = "123456"
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, new CancellationToken());
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual(result.Message, "Não foi encontrado nenhum usuário com esse e-mail.");
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnTrueWhenRequestIsInvalid()
    {
        var request = new Request()
        {
            Email = "john.doe@",
            VerificationCode = "1234"
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, new CancellationToken());

        Assert.IsFalse(result.Success);
    }
}