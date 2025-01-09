using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.ResendVerification.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.ResendVerification;

[TestClass]
public class HandlerTests
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public HandlerTests()
    {
        _repository = new MockRepository();
        _service = new MockService();
    }
    
    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnTrueWhenRequestIsValid()
    {
        var request = new Request
        {
            Email = "john.doe@gmail.com",
        };
        var handler = new Handler(_repository, _service);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenEmailDoesNotExist()
    {
        var request = new Request()
        {
            Email = "email@email.com"
        };
        var handler = new Handler(_repository, _service);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Não foi encontrado nenhum usuário com este e-mail.", result.Message);
    }
}