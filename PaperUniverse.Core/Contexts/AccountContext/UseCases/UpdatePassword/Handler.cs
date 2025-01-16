using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var validation = Specification.Assert(request);

        if (validation.IsValid == false)
            return new Response("Dados inválidos.", 400, validation.Notifications);

        User? user;

        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                return new Response("Não existe nenhum usuário com esse e-mail.", 404);
        }
        catch (Exception e)
        {
            return new Response("Houve um erro na hora de buscar os dados do usuário no banco.", 500);
        }
        
        user.ChangePassword(request.NewPassword);

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception e)
        {
            return new Response("Houve um problema no banco ao salvar os dados do usuário.", 500);
        }
        
        return new Response("Senha atualizada com sucesso.", 200);
    }
}