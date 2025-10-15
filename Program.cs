using Fiap.Web.Api.Desperdicio.Data;
using Fiap.Web.Api.Desperdicio.Data.Repository;
using Fiap.Web.Api.Desperdicio.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Necess�rio para ILogger

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Config DB

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<AppDbContext>(
    // Ativa logging para debug de queries
    opt => opt.UseNpgsql(connectionString).EnableSensitiveDataLogging(true)
    );

#endregion

#region Inje��es

// Registrar Repositories (Interface, Implementa��o)
builder.Services.AddScoped<IAlimentoRepository, AlimentoRepository>();
builder.Services.AddScoped<IImpactoRepository, ImpactoRepository>();

// Registrar Services (Interface, Implementa��o)
builder.Services.AddScoped<IAlimentoService, AlimentoService>();
// CORRE��O AQUI: IImpactoService deve ser mapeado para a classe ImpactoService
builder.Services.AddScoped<IImpactoService, ImpactoService>();

#endregion


var app = builder.Build();

// Bloco CR�TICO: Aplica as migra��es na inicializa��o do servi�o
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Esta linha for�a a cria��o do DB e das tabelas (agora como 'alimentos' e 'impactos')
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migra��es no PostgreSQL.");
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
