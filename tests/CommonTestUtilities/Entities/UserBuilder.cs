using Bogus;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enuns;
using CommonTestUtilities.Criptography;

namespace CommonTestUtilities.Entities
{
    public class UserBuilder
    {
        public  static User Build(string role = Roles.TEAM_MEMBER)
        {
            var password = new PasswordEcrypterBuilder().Build();

            var user = new Faker<User>()
                .RuleFor(u => u.Id, _=> 1)
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password,(_, user) => password.Encrypt(user.Password))
                .RuleFor(u => u.UserIdentifier, _ => Guid.NewGuid())
                .RuleFor(u => u.Role, _ => role);
            

                
            return user;
        }
    }
}
