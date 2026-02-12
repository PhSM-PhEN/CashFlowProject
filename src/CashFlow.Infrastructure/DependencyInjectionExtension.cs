using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)        // this e usado para criar um metodo de extensao
        {
           
            AddDataContexts(services, configuration);
            AddRepositories(services);

        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExpensesWriteOnlyRespository, ExpensesRepository>();//registra a implementação do repositório de despesas no contêiner de injeção de dependência
            services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();//registra a implementação do repositório de despesas no contêiner de injeção de dependência
            services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();//registra a implementação do repositório de despesas no contêiner de injeção de dependência
            services.AddScoped<IUnitOfWork, UnitOfWork>();    //registra a implementação da unidade de trabalho no contêiner de injeção de dependência
        }

        private static void AddDataContexts(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =  configuration.GetConnectionString("connection");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
