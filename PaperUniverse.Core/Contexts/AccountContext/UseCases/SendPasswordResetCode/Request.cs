using MediatR;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendPasswordResetCode;

public class Request : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
}