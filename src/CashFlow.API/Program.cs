using CashFlow.API.Filters;
using CashFlow.API.Middleware;
using CashFlow.Application.UseCases.ToExpenses;
using CashFlow.Infrastructure;
using CashFlow.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme. Example: , Bearer [space] token",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "outh2",
            Name = "Bearer",
            In = ParameterLocation.Header
            },
            new  List<string>()
        }

    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));//adiciona o filtro globalmente 

builder.Services.AddRouting(config => config.LowercaseUrls = true);//configura as rotas para serem em letras minúsculas
builder.Services.AddInfrastructureServices(builder.Configuration);//adiciona os serviços de infraestrutura ao contêiner de injeção de dependência
builder.Services.AddApplicationServices();//adiciona os serviços de aplicação ao contêiner de injeção de dependência


builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey")))

    };
});//adiciona os serviços de autenticação ao contêiner de injeção de dependência

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CultureInfoMiddleware>();//adiciona o middleware ao pipeline de requisições

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrateDataBase();

app.Run();

async Task MigrateDataBase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DataBaseMigration.MigrateDataBase(scope.ServiceProvider);


}
