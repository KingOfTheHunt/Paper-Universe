using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.ValueObjects;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnTrueWhenEmailIsValid()
    {
        var email = new Email("teste@email.com");
        
        Assert.IsTrue(email.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenEmailIsNotValid()
    {
        var email = new Email("teste.com");
        
        Assert.IsFalse(email.IsValid);
    }
}