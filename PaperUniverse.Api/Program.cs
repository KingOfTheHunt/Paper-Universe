using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using PaperUniverse.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.ConfigureSwagger();
builder.AddConfiguration();
builder.AddDatabase();
builder.AddAccountContext();
builder.AddMediatR();
builder.AddJwtAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAccountContextEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "I'm ready to work")
    .WithDescription("Endpoint para verificar se a API está funcionando.");

app.Run();
