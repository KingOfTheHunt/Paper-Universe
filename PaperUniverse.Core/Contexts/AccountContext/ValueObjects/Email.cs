using Flunt.Validations;
using PaperUniverse.Core.Contexts.SharedContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; private set; }
    public Verification Verification { get; private set; } = new();

    public Email(string address, string password)
    {
        Address = address;
        
        AddNotifications(new Contract<Email>()
            .Requires()
            .IsEmail(Address, "Address", "E-mail informado é inválido."));
    }
}