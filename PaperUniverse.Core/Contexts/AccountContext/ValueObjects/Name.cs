using Flunt.Validations;
using PaperUniverse.Core.Contexts.SharedContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    protected Name()
    {
    }
    
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        
        AddNotifications(new Contract<Name>()
            .Requires()
            .IsNotNullOrEmpty(FirstName, "FirstName", "O primeiro nome precisa ser informado.")
            .IsNotNullOrEmpty(LastName, "LastName", "O último nome precisa ser informado."));
    }
}