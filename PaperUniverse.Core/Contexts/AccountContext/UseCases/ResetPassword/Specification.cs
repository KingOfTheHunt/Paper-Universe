using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.ResetPassword;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido.")
            .IsGreaterOrEqualsThan(request.NewPassword.Length, 8, "NewPassword",
                "A senha precisa conter no mínimo 8 caracteres.")
            .AreEquals(request.ResetCode.Length, 6, "ResetCode",
                "O código não contém 6 caracteres.");
}