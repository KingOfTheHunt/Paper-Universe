using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido.")
            .IsGreaterOrEqualsThan(request.Password.Length, 8, "Password", 
                "A senha precisa conter no mínimo 8 caracteres.");
}