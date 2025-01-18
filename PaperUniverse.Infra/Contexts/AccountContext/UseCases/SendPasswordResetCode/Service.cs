using PaperUniverse.Core.Contexts;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts;
using PaperUniverse.Infra.Context.Helper;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.SendPasswordResetCode;

public class Service : IService
{
    public async Task SendEmailAsync(User user, CancellationToken cancellationToken)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var subject = "Resetar a senha!!!";
        var body = $@"Aqui está o código para que você possa resetar a sua senha.
                    <br/>
                    <strong>{user.Password.ResetCode}</strong>.";
        var mailMessage = MailHelper.CreateMailMessage(from, to, subject, body);
        await MailHelper.SendMailAsync(mailMessage, cancellationToken);
    }
}