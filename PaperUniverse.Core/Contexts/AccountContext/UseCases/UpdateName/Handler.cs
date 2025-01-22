using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName;

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
                return new Response("Não foi encontrado nenhum usuário com esse e-mail.", 404);
        }
        catch (Exception e)
        {
            return new Response("Houve um erro ao buscar o usuário no banco.", 500);
        }
        
        user.ChangeName(request.FirstName, request.LastName);

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception e)
        {
            return new Response("Houve um erro ao salvar os dados do usuário no banco.", 500);
        }
        
        return new Response("Alterado com sucesso!", 200);
    }
}