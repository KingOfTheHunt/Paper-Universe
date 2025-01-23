using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Delete.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Delete;

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

        try
        {
            var user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                return new Response("Não foi encontrado nenhum usuário com esse e-mail.", 404);
            
            await _repository.DeleteUserAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Houve um erro no banco de dados.", 500);
        }
        
        return new Response("Usuário deletado com sucesso.", 200);
    }
}