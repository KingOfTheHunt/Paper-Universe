using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido.")
            .IsNotNullOrEmpty(request.NewPassword, "NewPassword", "A nova senha precisa ser informada.")
            .IsNotNullOrEmpty(request.NewPasswordAgain, "NewPasswordAgain",
                "A nova senha precisa ser informada.")
            .IsGreaterOrEqualsThan(request.NewPassword.Length, 8, "NewPasssword",
                "A nova senha precisa ter 8 caracteres.")
            .IsGreaterOrEqualsThan(request.NewPasswordAgain.Length, 8, "NewPassswordAgain",
                "A nova senha precisa ter 8 caracteres.")
            .AreEquals(request.NewPassword, request.NewPasswordAgain, "NewPassword",
                "As senhas não são iguais.");
}