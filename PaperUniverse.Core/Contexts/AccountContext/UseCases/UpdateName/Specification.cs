using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido.")
            .IsNotNullOrEmpty(request.FirstName, "FirstName", "O primeiro nome deve ser informado.")
            .IsNotNullOrEmpty(request.LastName, "LastName", "O último nome deve ser informado.");
}