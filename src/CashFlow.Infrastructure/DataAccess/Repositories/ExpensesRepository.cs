
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expense;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    // deixar internal para não expor a implementação fora do projeto de infraestrutura
    internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRespository, IExpensesUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext = dbContext;


        public async Task Add(Expenses expense)
        {

            await _dbContext.Expenses.AddAsync(expense);

        }

        public async Task<bool> Delete(long id)
        {
            var result = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);

            if (result is null)
            {
                return false;
            }

            _dbContext.Expenses.Remove(result);

            return true;
        }

        public async Task<List<Expenses>> GetAll()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        }
        async Task<Expenses?> IExpensesReadOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
        async Task<Expenses?> IExpensesUpdateOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }
        public void Update(Expenses expense)
        {
            _dbContext.Expenses.Update(expense);
        }

        public async Task<List<Expenses>> FilterByMonth(DateOnly date)
        {
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            var endDate = new DateTime(year: date.Year, month: date.Month, daysInMonth, 23, minute: 59, second: 59);

            return await _dbContext.Expenses
                .AsNoTracking()
                .Where(expenses => expenses.Date >= startDate && expenses.Date <= endDate)
                .OrderBy(expense => expense.Date)
                .ToListAsync();
        }
    }
}
