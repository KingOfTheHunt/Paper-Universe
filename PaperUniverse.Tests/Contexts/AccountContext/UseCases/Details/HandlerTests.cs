using PaperUniverse.Core.Contexts.AccountContext.UseCases.Details;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Details.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.Details.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Details;

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
    public async Task ShouldReturnTrueWhenRequestIsValid()
    {
        var request = new Request()
        {
            Email = "john.doe@gmail.com",
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenEmailIsNotValid()
    {
        var request = new Request()
        {
            Email = "something that doesn't exist",
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual(400, result.Status);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenEmailDoesNotExist()
    {
        var request = new Request()
        {
            Email = "john.does@gmail.com",
        };
        var handler = new Handler(_repository);
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual(404, result.Status);
    }
}