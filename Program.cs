using MediatR;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Microsoft.Data.Sqlite;
using System.Data;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Microsoft.OpenApi.Models;
using Questao5.Application.Queries.Requests;
using Questao5.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<DatabaseConfig>();
    return new SqliteConnection(config.Name);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//injeção de dependencia
builder.Services.AddScoped<IContaQueryRequest, ContaQueryRequest>();
builder.Services.AddScoped<IMovimentacaoCommandRequest, MovimentarContaCommandRequest>();
builder.Services.AddScoped<IMovimentacaoQueryRequest, MovimentarContaQueryRequest>();
builder.Services.AddScoped<IConsultarSaldoQuery, ConsultarSaldoQuery>();
builder.Services.AddScoped<IDbExecutor, DapperDbExecutor>();

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

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


