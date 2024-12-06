using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;

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
        var requestValidation = Specification.Assert(request);

        if (requestValidation.IsValid == false)
            return new Response("Requisição inválida.", 400, requestValidation.Notifications);

        var emailExists = await _repository.AnyEmailAsync(request.Email, cancellationToken);

        if (emailExists)
            return new Response("O e-mail informado já existe em nossa base de dados.", 400);
        
        var name = new Name(request.FirstName, request.LastName);
        var email = new Email(request.Email);
        var password = new Password(request.Password);
        var user = new User(name, email, password);

        if (user.IsValid == false)
            return new Response("Os dados informados são inválidos.", 400, user.Notifications);

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Houve um erro ao salvar os dados no banco.", 500);
        }

        try
        {
            await _service.SendWelcomeEmail(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Não foi possível enviar o e-mail de boas vindas.", 500);
        }
        
        return new Response("Conta criada com sucesso.", 201);
    }
}