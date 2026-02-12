using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expense
{

    public interface IExpensesWriteOnlyRespository
    {
        Task Add(Expenses expense);

        /// <summary>
        /// This function return TRUE if the deletion was successful and FALSE if the entity was not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(long id);
    }

}
