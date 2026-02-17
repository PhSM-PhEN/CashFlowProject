using Bogus;
using CashFlow.Communication.Request.ToLogin;

namespace CommonTestUtilities.Resquests
{

    public class RequestLoginJsonBuilder
    {
        public static RequestLoginJson Build()
        {
            return new Faker<RequestLoginJson>()
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "Aa1"));
        }

    }
}
