using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResetPassword.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.ResetPassword;

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
        catch (Exception)
        {
            return new Response("Houve um erro ao buscar o usuário no banco", 500);
        }
        
        user.UpdatePassword(request.ResetCode, request.NewPassword);

        if (user.IsValid == false)
            return new Response("O código informado não é válido.", 400);

        try
        {
            await _repository.SaveUserAsync(user, cancellationToken);
        }
        catch (Exception e)
        {
            return new Response("Houve um erro ao salvar os dados do usuário.", 500);
        }
        
        return new Response("Senha alterada com sucesso.", 200);
    }
}