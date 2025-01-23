using System.Text.Json.Serialization;
using MediatR;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdateName;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}