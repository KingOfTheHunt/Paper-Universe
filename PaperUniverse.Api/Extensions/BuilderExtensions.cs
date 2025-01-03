using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PaperUniverse.Core.Contexts;
using PaperUniverse.Infra.Data;

namespace PaperUniverse.Api.Extensions;

public static class BuilderExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString = builder.Configuration
            .GetConnectionString("Default") ?? string.Empty;

        Configuration.Smtp.Host = builder.Configuration.GetSection("SMTP").GetValue<string>("Host")
            ?? string.Empty;
        Configuration.Smtp.Port = builder.Configuration.GetSection("SMTP").GetValue<int>("Port");
        Configuration.Smtp.Login = builder.Configuration.GetSection("SMTP").GetValue<string>("Login")
            ?? string.Empty;
        Configuration.Smtp.Password = builder.Configuration.GetSection("SMTP").GetValue<string>("Password")
            ?? string.Empty;
        Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.Database.ConnectionString, x => 
                x.MigrationsAssembly("PaperUniverse.Api"));
        });
    }

    public static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(typeof(Configuration).Assembly));
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization();
    }
}