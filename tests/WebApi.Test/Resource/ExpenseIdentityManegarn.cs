namespace WebApi.Test.Resource
{
    public class ExpenseIdentityManegarn(CashFlow.Domain.Entities.Expenses expense)
    {
        private readonly CashFlow.Domain.Entities.Expenses _expense = expense;


        public long GetById() => _expense.Id;
        public DateTime GetDate() => _expense.Date;



    }
}
