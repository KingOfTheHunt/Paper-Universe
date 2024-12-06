using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Assert(Request request)
    {
        return new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.FirstName, "FirstName", "O primeiro nome precisa ser informado.")
            .IsNotNullOrEmpty(request.LastName, "LastName", "O último nome precisa ser informado.")
            .IsEmail(request.Email, "Email", "E-mail informado inválido.")
            .IsGreaterOrEqualsThan(request.Password.Length, 8, 
                "Password", "A senha precisa de no mínimo 8 caracteres.");
    }
}