using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CashFlow.Infrastructure.DataAccess
{
    // deixar internal para não expor a implementação fora do projeto de infraestrutura
    internal class CashFlowDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Expenses> Expenses { get; set; }

        
    }
}


