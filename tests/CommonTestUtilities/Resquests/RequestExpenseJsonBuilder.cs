using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Request.ToExpenses;

namespace CommonTestUtilities.Resquests
{
    public class RequestExpenseJsonBuilder
    {
        public static RequestExpenseJson Build() 
        {



           return new Faker<RequestExpenseJson>()
                .RuleFor(r => r.Title, f => f.Commerce.ProductName())
                .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
                .RuleFor(r => r.Date, f => f.Date.Past())
                .RuleFor(r => r.Amount, f => f.Finance.Amount(min: 1, max: 1000))
                .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentTypeEnum>())
                .Generate();

           
        }
    }

}
