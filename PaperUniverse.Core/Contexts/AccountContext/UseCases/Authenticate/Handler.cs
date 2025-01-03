using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;

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
            return new Response("Não foi possível obter os dados do banco.", 500);
        }

        if (user.Email.Verification.Active == false)
            return new Response("A conta não foi ativada.", 400);
        
        if (user.Password.Challenge(request.Password) == false)
            return new Response("Senha incorreta.", 401);

        var data = new Response.ResponseData()
        {
            Email = user.Email.Address,
            Name = $"{user.Name.FirstName} {user.Name.LastName}"
        };
        
        return new Response("Autenticado com sucesso.", 200, data);
    }
}