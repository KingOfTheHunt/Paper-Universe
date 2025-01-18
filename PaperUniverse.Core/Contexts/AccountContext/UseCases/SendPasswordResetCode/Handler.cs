using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
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
        catch (Exception)
        {
            return new Response("Houve um problema no banco de dados.", 500);
        }

        await _service.SendEmailAsync(user, cancellationToken);
        
        return new Response("Código enviado com sucesso.", 200);
    }
}