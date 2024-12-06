using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using A2Avaliacao.Data;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<A2AvaliacaoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("A2AvaliacaoContext") ?? throw new InvalidOperationException("Connection string 'A2AvaliacaoContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Avaliação A2 - Esther",
            Description = "API DO PROJETO PARA AVALIAÇÃO A2",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "API de Tarefas",
                Email = "estherreis@unitins.br",
                Url = new Uri("https://unitins.br")
            }
        });

    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);

    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
