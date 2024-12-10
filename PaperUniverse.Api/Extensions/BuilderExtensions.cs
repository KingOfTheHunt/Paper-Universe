using Microsoft.EntityFrameworkCore;
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
}