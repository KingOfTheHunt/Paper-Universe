using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.SendPasswordResetCode.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.SendPasswordResetCode;

[TestClass]
public class HandlerTests
{
    private readonly IRepository _repository;
    private readonly IService _service;
    private readonly Request _invalidRequest, _validRequest;

    public HandlerTests()
    {
        _repository = new MockRepository();
        _service = new MockService();
        _invalidRequest = new Request() { Email = "email.com" };
        _validRequest = new Request() { Email = "john.doe@gmail.com" };
    }
    
    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenRequestIsInvalid()
    {
        var handler = new Handler(_repository, _service);
        var result = await handler.Handle(_invalidRequest, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnTrueWhenRequestIsValid()
    {
        var handler = new Handler(_repository, _service);
        var result = await handler.Handle(_validRequest, CancellationToken.None);
        
        Assert.IsTrue(result.Success);
    }
}