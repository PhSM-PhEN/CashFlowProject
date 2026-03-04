using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CashFlow.Infrastructure.DataAccess
{
    
    public class CashFlowDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>().ToTable("Tags");
        }


    }
}


