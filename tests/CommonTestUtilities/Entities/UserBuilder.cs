using Bogus;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Criptography;

namespace CommonTestUtilities.Entities
{
    public class UserBuilder
    {
        public  static User Build()
        {
            var password = new PasswordEcrypterBuilder().Build();

            var user = new Faker<User>()
                .RuleFor(u => u.Id, _=> 1)
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password,(_, user) => password.Encrypt(user.Password))
                .RuleFor(u => u.UserIdentifier, _ => Guid.NewGuid())
                ;

                
            return user;
        }
    }
}
