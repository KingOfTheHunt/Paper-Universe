using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Details.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Details;

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
            user = await _repository.GetUserAsync(request.Email);

            if (user is null)
                return new Response("Não foi encontrado nenhum usuário com este e-mail.", 404);
        }
        catch (Exception)
        {
            return new Response("Houve um erro no banco de dados.", 500);
        }

        var data = new ResponseData()
        {
            Name = user.Name.ToString(),
            Email = user.Email.Address
        };
        
        return new Response("Dados obtidos com sucesso.", 200, data);
    }
}