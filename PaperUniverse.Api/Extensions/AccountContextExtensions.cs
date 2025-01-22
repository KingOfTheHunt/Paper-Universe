using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;

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
        
        #region Authenticate
        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.Authenticate.Repository
        >();
        
        #endregion

        #region Resend Verification

        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.ResendVerification.Repository
        >();

        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts.IService,
            Infra.Contexts.AccountContext.UseCases.ResendVerification.Service
        >();

        #endregion

        #region Details

        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.Details.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.Details.Repository
        >();

        #endregion
        
        #region Update Password

        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.UpdatePassword.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.UpdatePassword.Repository
        >();

        #endregion

        #region Send Password Reset Code

        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.SendPasswordResetCode.Repository
        >();

        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Contracts.IService,
            Infra.Contexts.AccountContext.UseCases.SendPasswordResetCode.Service
        >();

        #endregion

        #region Reset Password

        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.ResetPassword.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.ResetPassword.Repository
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
        
        app.MapPost("v1/users/validate", async (
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

        #region Authenticate

        app.MapPost("v1/users/authenticate", async (
                Core.Contexts.AccountContext.UseCases.Authenticate.Request req,
                IRequestHandler<Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                    Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
            {
                var result = await handler.Handle(req, new CancellationToken());

                if (result.Success == false)
                    return Results.Json(result, statusCode: result.Status);

                if (result.Data is not null)
                    result.Data.Token = JwtExtensions.Generate(result.Data);

                return Results.Json(result, statusCode: result.Status);
            })
            .WithTags("Users")
            .WithDescription("Gera o token de autenticação.")
            .Produces(StatusCodes.Status200OK,
                typeof(Core.Contexts.AccountContext.UseCases.Authenticate.Response))
            .Produces(StatusCodes.Status400BadRequest, 
                typeof(Core.Contexts.AccountContext.UseCases.Authenticate.Response))
            .Produces(StatusCodes.Status401Unauthorized, 
                typeof(Core.Contexts.AccountContext.UseCases.Authenticate.Response))
            .Produces(StatusCodes.Status404NotFound, 
                typeof(Core.Contexts.AccountContext.UseCases.Authenticate.Response))
            .Produces(StatusCodes.Status500InternalServerError, 
                typeof(Core.Contexts.AccountContext.UseCases.Authenticate.Response));

        #endregion

        #region Resend Verification

        app.MapPost("v1/users/resend-verification", async (
                Core.Contexts.AccountContext.UseCases.ResendVerification.Request request,
                IRequestHandler<Core.Contexts.AccountContext.UseCases.ResendVerification.Request,
                    Core.Contexts.AccountContext.UseCases.ResendVerification.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.Status);
            })
            .WithTags("Users")
            .WithDescription("Gera um novo código de verificação e o envia para o e-mail do usuário.")
            .Produces(StatusCodes.Status200OK,
                typeof(Core.Contexts.AccountContext.UseCases.ResendVerification.Response))
            .Produces<Core.Contexts.AccountContext.UseCases.ResendVerification.Response>(
                StatusCodes.Status400BadRequest)
            .Produces<Core.Contexts.AccountContext.UseCases.ResendVerification.Response>(
                StatusCodes.Status404NotFound)
            .Produces<Core.Contexts.AccountContext.UseCases.ResendVerification.Response>(
                StatusCodes.Status500InternalServerError);

        #endregion

        #region Details

        app.MapPost("v1/users/details", async (
                HttpContext httpContext,
                Core.Contexts.AccountContext.UseCases.Details.Request request,
                IRequestHandler<Core.Contexts.AccountContext.UseCases.Details.Request,
                    Core.Contexts.AccountContext.UseCases.Details.Response> handler) =>
            {
                if (httpContext.User.Identity?.IsAuthenticated == false)
                    return Results.Json(
                        new Core.Contexts.AccountContext.UseCases
                            .Details.Response("Usuário não autenticado!", 401), 
                            statusCode: StatusCodes.Status401Unauthorized);

                if (httpContext.User.Identity?.Name != request.Email)
                    return Results.BadRequest();
                        
                var result = await handler.Handle(request, new CancellationToken());

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.Status);
            }).RequireAuthorization()
            .WithTags("Users")
            .WithDescription("Retorna os dados do usuário autenticado.")
            .Produces<Core.Contexts.AccountContext.UseCases.Details.Response>(
                StatusCodes.Status200OK)
            .Produces<Core.Contexts.AccountContext.UseCases.Details.Response>(
                StatusCodes.Status400BadRequest)
            .Produces<Core.Contexts.AccountContext.UseCases.Details.Response>(
                StatusCodes.Status401Unauthorized)
            .Produces<Core.Contexts.AccountContext.UseCases.Details.Response>(
                StatusCodes.Status404NotFound)
            .Produces<Core.Contexts.AccountContext.UseCases.Details.Response>(
                StatusCodes.Status500InternalServerError);

        #endregion

        #region Update Password

        app.MapPut("v1/users/update-password", async (HttpContext httpContext,
                Core.Contexts.AccountContext.UseCases.UpdatePassword.Request request,
                IRequestHandler<Core.Contexts.AccountContext.UseCases.UpdatePassword.Request,
                    Core.Contexts.AccountContext.UseCases.UpdatePassword.Response> handler) =>
            {
                if (httpContext.User.Identity?.IsAuthenticated == false)
                    return Results.Unauthorized();

                request.Email = httpContext.User.Identity.Name;
                var result = await handler.Handle(request, new CancellationToken());

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.Status);
            })
            .RequireAuthorization()
            .WithTags("Users")
            .WithDescription("Atualiza a senha do usuário.")
            .Produces<Core.Contexts.AccountContext.UseCases.UpdatePassword.Response>
                (StatusCodes.Status200OK)
            .Produces<Core.Contexts.AccountContext.UseCases.UpdatePassword.Response>
                (StatusCodes.Status400BadRequest)
            .Produces<Core.Contexts.AccountContext.UseCases.UpdatePassword.Response>
                (StatusCodes.Status401Unauthorized)
            .Produces<Core.Contexts.AccountContext.UseCases.UpdatePassword.Response>
                (StatusCodes.Status500InternalServerError);

        #endregion

        #region Send Password Reset Code

        app.MapPost("v1/users/send-password-reset-code", async (
                Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Request request,
                IRequestHandler<Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Request,
                    Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.Status);
            })
            .WithTags("Users")
            .WithDescription("Envia o código para resetar a senha do usuário.")
            .Produces<Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Response>
                (StatusCodes.Status200OK)
            .Produces<Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Response>
                (StatusCodes.Status400BadRequest)
            .Produces<Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Response>
                (StatusCodes.Status404NotFound)
            .Produces<Core.Contexts.AccountContext.UseCases.SendPasswordResetCode.Response>
                (StatusCodes.Status500InternalServerError);


        #endregion

        #region Reset Password

        app.MapPut("v1/users/reset-password", async (
                Core.Contexts.AccountContext.UseCases.ResetPassword.Request request,
                IRequestHandler<Core.Contexts.AccountContext.UseCases.ResetPassword.Request,
                    Core.Contexts.AccountContext.UseCases.ResetPassword.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.Status);
            })
            .WithTags("Users")
            .WithDescription("Redefine a senha do usuário.")
            .Produces<Core.Contexts.AccountContext.UseCases.ResetPassword.Response>
                (StatusCodes.Status200OK)
            .Produces<Core.Contexts.AccountContext.UseCases.ResetPassword.Response>
                (StatusCodes.Status400BadRequest)
            .Produces<Core.Contexts.AccountContext.UseCases.ResetPassword.Response>
                (StatusCodes.Status404NotFound)
            .Produces<Core.Contexts.AccountContext.UseCases.ResetPassword.Response>
                (StatusCodes.Status500InternalServerError);

        #endregion
    }
}