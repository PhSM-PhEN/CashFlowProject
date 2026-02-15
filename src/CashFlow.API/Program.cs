using CashFlow.API.Filters;
using CashFlow.API.Middleware;
using CashFlow.Application.UseCases.ToExpenses;
using CashFlow.Infrastructure;
using CashFlow.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));//adiciona o filtro globalmente 

builder.Services.AddRouting(config => config.LowercaseUrls = true);//configura as rotas para serem em letras minúsculas
builder.Services.AddInfrastructureServices(builder.Configuration);//adiciona os serviços de infraestrutura ao contêiner de injeção de dependência
builder.Services.AddApplicationServices();//adiciona os serviços de aplicação ao contêiner de injeção de dependência


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CultureInfoMiddleware>();//adiciona o middleware ao pipeline de requisições

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await MigrateDataBase();

app.Run();

async Task MigrateDataBase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DataBaseMigration.MigrateDataBase(scope.ServiceProvider);


}
