using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.ValueObjects;

[TestClass]
public class PasswordTests
{
    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnTrueWhenPasswordIsValid()
    {
        var password = new Password("123456789");
        
        Assert.IsTrue(password.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenPasswordHasLessThan8Characters()
    {
        var password = new Password("1234");
        
        Assert.IsFalse(password.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenPasswordIsNullOrEmpty()
    {
        var password = new Password("");
        
        Assert.IsFalse(password.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnTrueWhenPasswordIsCorrect()
    {
        var password = new Password("123456789");
        var result = password.Challenge("123456789");
        
        Assert.IsTrue(result);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenPasswordIsWrong()
    {
        var password = new Password("123456789");
        var result = password.Challenge("12345678");
        
        Assert.IsFalse(result);
    }
}