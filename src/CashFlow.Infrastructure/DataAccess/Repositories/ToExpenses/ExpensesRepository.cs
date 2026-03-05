using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CashFlow.Infrastructure.DataAccess.Repositories.ToExpenses
{
    // deixar internal para não expor a implementação fora do projeto de infraestrutura
    internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRespository, IExpensesUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext = dbContext;


        public async Task Add(Expenses expense)
        {

            await _dbContext.Expenses.AddAsync(expense);

        }

        public async Task Delete(long id)
        {
            var result = await _dbContext.Expenses.FindAsync(id);

            _dbContext.Expenses.Remove(result!);

            
        }

        public async Task<List<Expenses>> GetAll(Domain.Entities.User user)
        {
            return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.UserId == user.Id).ToListAsync();
        }
        async Task<Expenses?> IExpensesReadOnlyRepository.GetById(Domain.Entities.User user ,long id)
        {
            return await GetFullExpenses()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
        }
        async Task<Expenses?> IExpensesUpdateOnlyRepository.GetById(Domain.Entities.User user,long id)
        {
            return await GetFullExpenses()
                .FirstOrDefaultAsync(e => e.Id == id  && e.UserId == user.Id);
        }
        public void Update(Expenses expense)
        {
            _dbContext.Expenses.Update(expense);
        }

        public async Task<List<Expenses>> FilterByMonth(Domain.Entities.User user,DateOnly date)
        {
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            var endDate = new DateTime(year: date.Year, month: date.Month, daysInMonth, 23, minute: 59, second: 59);

            return await _dbContext.Expenses
                .AsNoTracking()
                .Where( expenses => expenses.UserId == user.Id && expenses.Date >= startDate && expenses.Date <= endDate)
                .OrderBy(expense => expense.Date)
                .ToListAsync();
        }
        private IIncludableQueryable<Expenses, ICollection<Tag>> GetFullExpenses()
        {
            return _dbContext.Expenses
                .Include(expense => expense.Tags);
                
        }
    }
}
