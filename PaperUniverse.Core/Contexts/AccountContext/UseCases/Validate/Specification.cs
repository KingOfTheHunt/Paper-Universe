using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Validate;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido.")
            .AreEquals(request.VerificationCode.Length, 6, "VerificationCode",
                "Código de verificação inválido.");
}