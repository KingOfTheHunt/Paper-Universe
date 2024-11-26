using PaperUniverse.Core.Contexts.SharedContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; private set; } = Guid.NewGuid().ToString("N")[..6];
    public DateTime? ExpiresAt { get; private set; } = DateTime.Now.AddMinutes(30);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool Active => ExpiresAt == null && VerifiedAt != null;


    public void Verify(string code)
    {
        if (Active)
        {
            AddNotification("Active", "A conta já foi verificada.");
            
            return;
        }

        if (ExpiresAt < DateTime.Now)
        {
            AddNotification("ExpiresAt", "O código de ativação expirou.");
            
            return;
        }

        if (string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase) == false)
        {
            AddNotification("Code", "O código informado está incorreto.");

            return;
        }

        ExpiresAt = null;
        VerifiedAt = DateTime.Now;
    }
    
}