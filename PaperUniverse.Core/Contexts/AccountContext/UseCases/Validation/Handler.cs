using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Validation.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Validation;

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
            user = await _repository.GetUserByEmailAsync(request.Email);

            if (user is null)
                return new Response("Não foi encontrado nenhum usuário com esse e-mail.", 404);
        }
        catch (Exception)
        {
            return new Response("Houve um erro na hora de buscar o usuário.", 500);
        }
        
        user.Email.Verification.Verify(request.VerificationCode);
        
        if (user.Email.Verification.IsValid == false)
            return new Response("Não foi possível verificar o e-mail.", 400, 
                user.Email.Verification.Notifications);

        try
        {
            await _repository.SaveAsync(user);
        }
        catch (Exception)
        {
            return new Response("Houve um erro na hora de salvar as alterações.", 500);
        }
        
        return new Response("E-mail verificado com sucesso.", 200);
    }
}