using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Tests.Contexts.AccountContext.ValueObjects;

[TestClass]
public class VerificationTests
{
    private readonly Verification _verification;

    public VerificationTests()
    {
        _verification = new Verification();
    }
    
    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnTrueWhenVerificationCodeIsValid()
    {
        var code = _verification.Code;
        _verification.Verify(code);
        
        Assert.IsTrue(_verification.Active);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenVerificationCodeIsNotValid()
    {   
        _verification.Verify("14af48");
        
        Assert.IsFalse(_verification.Active);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenAccountIsAlreadyVerified()
    {
        var code = _verification.Code;
        _verification.Verify(code);
        _verification.Verify(code);
        string message = "";

        foreach (var notification in _verification.Notifications)
        {
            message = notification.Message;
        }
        
        Assert.IsFalse(_verification.IsValid);
        Assert.AreEqual(message, "A conta já foi verificada.");
    }
}