using PaperUniverse.Core.Contexts;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using PaperUniverse.Infra.Context.Helper;

namespace PaperUniverse.Infra.Context.AccountContext.UseCases.Create;

public class Service : IService
{
    public async Task SendWelcomeEmail(User user, CancellationToken cancellationToken)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var subject = $"Bem vindo ao PaperUniverse, {user.Name.ToString()}";
        var body = $@"Seja muito bem vindo ao PaperUniverse!
                    <br/><br/>
                    Para usar a sua conta é necessário ativa-la, e para isso basta informar o código abaixo.
                    <br/>
                    Seu código: <strong>{user.Email.Verification.Code}</strong>.
                    <br/>
                    Você tem até as {user.Email.Verification.ExpiresAt} para ativar a sua conta.";
        
        var mailMessage = MailHelper.CreateMailMessage(from, to, subject, body);
        await MailHelper.SendMailAsync(mailMessage, cancellationToken);
    }
}