using PaperUniverse.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(y => y.FullName);
});
builder.AddConfiguration();
builder.AddDatabase();
builder.AddAccountContext();
builder.AddMediatR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAccountContextEndpoints();

app.MapGet("/", () => "I'm ready to work")
    .WithDescription("Endpoint para verificar se a API está funcionando.");

app.Run();
