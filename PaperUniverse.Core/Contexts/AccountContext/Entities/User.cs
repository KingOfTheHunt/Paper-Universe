using Flunt.Validations;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;
using PaperUniverse.Core.Contexts.SharedContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    protected User()
    {
    }
    
    public User(Name name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
        
        AddNotifications(new Contract<User>()
            .Requires()
            .IsNotNull(Name, "Name", "O nome precisa ser informado.")
            .IsNotNull(Email, "Email", "O e-mail precisa ser informado.")
            .IsNotNull(Password, "Password", "A senha precisa ser informada."));
        
        if (Name is not null && Email is not null && Password is not null)
            AddNotifications(Name, Email, Password);
    }

    public void UpdatePassword(string resetCode, string newPassword)
    {
        if (string.Equals(Password.ResetCode.Trim(), resetCode.Trim(),
                StringComparison.CurrentCultureIgnoreCase) == false)
        {
            AddNotification("ResetCode", "O código informado é inválido.");

            return;
        }

        Password = new Password(newPassword);
    }
    
    public void ChangePassword(string newPassword)
    {
        var password = new Password(newPassword);
        Password = password;
    }

    public void ChangeName(string firstName, string lastName)
    {
        Name = new Name(firstName, lastName);
    }
}