using MediatR;

namespace PaperUniverse.Api.Extensions;

public static class AccountContextExtensions
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            Infra.Context.AccountContext.UseCases.Create.Repository>();
        builder.Services.AddTransient<Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            Infra.Context.AccountContext.UseCases.Create.Service>();

        #endregion
    }

    public static void MapAccountContextEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("v1/users/create", async (Core.Contexts.AccountContext.UseCases.Create.Request req,
                IRequestHandler<
                    Core.Contexts.AccountContext.UseCases.Create.Request,
                    Core.Contexts.AccountContext.UseCases.Create.Response
                > handler) =>
            {
                var result = await handler.Handle(req, new CancellationToken());

                if (result.Success)
                    return Results.Created("", result);

                return Results.BadRequest(result);
            })
            .WithTags("Users")
            .Produces(StatusCodes.Status201Created,
                typeof(Core.Contexts.AccountContext.UseCases.Create.Response))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        #endregion
    }
}