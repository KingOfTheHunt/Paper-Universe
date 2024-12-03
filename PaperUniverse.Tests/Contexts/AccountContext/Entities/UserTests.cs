using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.Entities;

[TestClass]
public class UserTests
{
    private readonly Name _name;
    private readonly Email _email;
    private readonly Password _password;

    public UserTests()
    {
        _name = new("John", "Doe");
        _email = new("johndoe@gmail.com");
        _password = new("12345678");
    }
    
    [TestMethod]
    [TestCategory("Entities")]
    public void ShouldReturnTrueWhenUserIsValid()
    {
        var user = new User(_name, _email, _password);
        
        Assert.IsTrue(user.IsValid);
    }

    [TestMethod]
    [TestCategory("Entities")]
    public void ShouldReturnFalseWhenUserIsInvalid()
    {
        var user = new User(null, null, null);
        
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("Entities")]
    public void ShouldReturnFalseWhenUserNameIsNull()
    {
        var user = new User(null, _email, _password);

        var notification = user.Notifications.FirstOrDefault();
        
        Assert.IsFalse(user.IsValid);
        Assert.AreEqual("O nome precisa ser informado.", notification?.Message);
    }

    [TestMethod]
    [TestCategory("Entities")]
    public void ShouldReturnFalseWhenEmailIsNull()
    {
        var user = new User(_name, null, _password);
        var notification = user.Notifications.FirstOrDefault();
        
        Assert.IsFalse(user.IsValid);
        Assert.AreEqual("O e-mail precisa ser informado.", notification?.Message);
    }

    [TestMethod]
    [TestCategory("Entities")]
    public void ShouldReturnFalseWhenPasswordIsNull()
    {
        var user = new User(_name, _email, null);
        var notification = user.Notifications.FirstOrDefault();
        
        Assert.IsFalse(user.IsValid);
        Assert.AreEqual("A senha precisa ser informada.", notification?.Message);
    }
}