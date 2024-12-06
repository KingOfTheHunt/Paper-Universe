using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.Create.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.Create;

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
    public async Task ShouldCreateAccountWhenRequestIsValid()
    {
        var request = new Request
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            Password = "password",
        };
        var handler = new Handler(_repository, _service);
        var result = await handler.Handle(request, new CancellationToken());
        
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldNotCreateAccountWhenEmailAlreadyExists()
    {
        var request = new Request
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "test@test.com",
            Password = "password"
        };
        var handler = new Handler(_repository, _service);
        var result = await handler.Handle(request, new CancellationToken());
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual(result.Message, "O e-mail informado já existe em nossa base de dados.");
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldNotCreateAccountWhenRequestIsInvalid()
    {
        var request = new Request();
        var handler = new Handler(_repository, _service);
        var result = await handler.Handle(request, new CancellationToken());
        
        Assert.IsFalse(result.Success);
    }
}