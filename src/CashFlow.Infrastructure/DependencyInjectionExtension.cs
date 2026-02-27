using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Repositories.ToUser;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories.ToExpenses;
using CashFlow.Infrastructure.DataAccess.Repositories.User;
using CashFlow.Infrastructure.Extensions;
using CashFlow.Infrastructure.Security.Token;
using CashFlow.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)        // this e usado para criar um metodo de extensao
        {

            services.AddScoped<IPasswordEncripter, Security.Criptography.BCrypt>(); //registra a implementação do encriptador de senha no contêiner de injeção de dependência
            services.AddScoped<ILoggedUser, LoggedUser>(); //registra a implementação do serviço de usuário logado no contêiner de injeção de dependência

            AddToken(services, configuration);
            AddRepositories(services);
            
            if(configuration.IsTestiEnviroment() == false)
            {
                AddDataContexts(services, configuration);
            }

            

        }
        private static void AddToken( IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));

        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExpensesWriteOnlyRespository, ExpensesRepository>();//registra a implementação do repositório de despesas no contêiner de injeção de dependência
            services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();//registra a implementação do repositório de despesas no contêiner de injeção de dependência
            services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();//registra a implementação do repositório de despesas no contêiner de injeção de dependência
            services.AddScoped<IUnitOfWork, UnitOfWork>();    //registra a implementação da unidade de trabalho no contêiner de injeção de dependência
            services.AddScoped<IUserReadOnlyRespository, UserRespository>();//registra a implementação do repositório de usuário no contêiner de injeção de dependência
            services.AddScoped<IUserWriteOnlyRespository, UserRespository>();//registra a implementação do repositório de usuário no contêiner de injeção de dependência
            services.AddScoped<IUserUpdateOnlyRepository, UserRespository>();

        }

        private static void AddDataContexts(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =  configuration.GetConnectionString("connection");

            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
