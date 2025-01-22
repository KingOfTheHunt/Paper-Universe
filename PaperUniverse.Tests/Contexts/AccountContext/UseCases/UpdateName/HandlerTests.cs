using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName.Contracts;
using PaperUniverse.Tests.Contexts.AccountContext.UseCases.UpdateName.Mocks;

namespace PaperUniverse.Tests.Contexts.AccountContext.UseCases.UpdateName;

[TestClass]
public class HandlerTests
{
    private readonly IRepository _repository;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repository = new MockRepository();
        _handler = new Handler(_repository);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenFirstNameIsNull()
    {
        var request = new Request
        {
            Email = "john.smith@gmail.com",
            FirstName = null,
            LastName = "Smith",
        };
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Dados inválidos.", result.Message);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenLastNameIsNull()
    {
        var request = new Request()
        {
            Email = "john.smith@gmail.com",
            FirstName = "John",
            LastName = null,
        };
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Dados inválidos.", result.Message);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenEmailDoesNotExist()
    {
        var request = new Request()
        {
            Email = "john.doe@gmail.com",
            FirstName = "John",
            LastName = "Doe",
        };
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Não foi encontrado nenhum usuário com esse e-mail.", result.Message);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public async Task ShouldReturnFalseWhenRequestIsValid()
    {
        var request = new Request()
        {
            Email = "john.smith@gmail.com",
            FirstName = "Johnny",
            LastName = "Doe"
        };
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Alterado com sucesso!", result.Message);
    }
    
}