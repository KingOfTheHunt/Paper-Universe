using PaperUniverse.Core.Contexts;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;
using PaperUniverse.Infra.Context.Helper;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.ResendVerification;

public class Service : IService
{
    public async Task SendEmailAsync(User user)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var subject = "Reenvio do código de verificação";
        var body = $@"Aqui está o seu novo código, {user.Name}.
            <br/>
            Código:<strong>{user.Email.Verification.Code}</strong>.
            <br/>
            Você tem até as {user.Email.Verification.ExpiresAt} para verificar a sua conta.";

        var mail = MailHelper.CreateMailMessage(from, to, subject, body);
        await MailHelper.SendMailAsync(mail, new CancellationToken());
    }
}