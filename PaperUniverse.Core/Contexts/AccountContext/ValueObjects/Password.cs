using Flunt.Validations;
using PaperUniverse.Core.Contexts.SharedContext.ValueObjects;
using SecureIdentity.Password;

namespace PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

public class Password : ValueObject
{
    public string Hash { get; private set; }
    public string ResetCode { get; set; } = Guid.NewGuid().ToString("N")[..6];

    public Password(string password)
    {
        AddNotifications(new Contract<Password>()
            .Requires()
            .IsNotNullOrEmpty(password, "Password", "A senha precisa ser informada.")
            .IsGreaterThan(password.Length, 8, "Password", "A senha precisa no mínimo 8 " +
                                                           "caracteres."));
        
        Hash = PasswordHasher.Hash(password, privateKey: Configuration.PrivateKey);
    }

    public bool Challenge(string plainTextPassword) => 
        PasswordHasher.Verify(Hash, plainTextPassword, privateKey: Configuration.PrivateKey);
}