using Bogus;
using CashFlow.Domain.Entities;

namespace CommonTestUtilities.ToExpense
{
    public class ExpenseBuilder
    {
        public static List<Expenses> Collection(User user, uint count = 2)
        {
            var list = new List<Expenses>();
            if (count == 0)
            {
                count = 1;
            }
            var expenseId = 1;

            for (int  i = 0;  i < count;  i++)
            {
                var expense = Build(user);
                expense.Id = expenseId++;
                list.Add(expense);
            }
            return list;
        }


        public static  Expenses Build(User user)
        {
            return new Faker<Expenses>()
                .RuleFor(exp => exp.Id, _ = 1)
                .RuleFor(exp => exp.Title, f => f.Commerce.ProductName())
                .RuleFor(exp => exp.Description, f => f.Commerce.ProductDescription())
                .RuleFor(exp => exp.Amount, f => f.Random.Decimal(1, 2000))
                .RuleFor(exp => exp.Date, f => f.Date.Past(1))
                .RuleFor(exp => exp.PaymentType, f => f.PickRandom<CashFlow.Domain.Enuns.PaymentTypeEnum>())
                .RuleFor(exp => exp.UserId, _ = user.Id);


        }
    }
}
