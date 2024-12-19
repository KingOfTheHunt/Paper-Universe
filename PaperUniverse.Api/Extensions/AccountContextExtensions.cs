using MediatR;

namespace PaperUniverse.Api.Extensions;

public static class AccountContextExtensions
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.Create.Repository>();
        builder.Services.AddTransient<Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            Infra.Contexts.AccountContext.UseCases.Create.Service>();

        #endregion

        #region Validation
        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.Validate.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.Validate.Repository
        >();
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

        #region Validation

        app.MapPost("v1/users/validation", async (
                Core.Contexts.AccountContext.UseCases.Validate.Request req,
                IRequestHandler<
                    Core.Contexts.AccountContext.UseCases.Validate.Request,
                    Core.Contexts.AccountContext.UseCases.Validate.Response> handler) =>
            {
                var result = await handler.Handle(req, new CancellationToken());

                if (result.Success)
                    return Results.Ok(result);

                return Results.BadRequest(result);
            })
            .WithTags("Users")
            .WithDescription("Ativa a conta do usuário.")
            .Produces(StatusCodes.Status200OK,
                typeof(Core.Contexts.AccountContext.UseCases.Validate.Response))
            .Produces(StatusCodes.Status404NotFound,
                typeof(Core.Contexts.AccountContext.UseCases.Validate.Response))
            .Produces(StatusCodes.Status400BadRequest,
                typeof(Core.Contexts.AccountContext.UseCases.Validate.Response));

        #endregion
    }
}