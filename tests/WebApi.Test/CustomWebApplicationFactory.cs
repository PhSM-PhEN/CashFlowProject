using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using CashFlow.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using CommonTestUtilities.ToExpense;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resource;


namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public ExpenseIdentityManegarn EXPENSE_MEMBER_TIME { get; private set; } = default!;
        public ExpenseIdentityManegarn EXPENSE_ADMIN { get; private set; } = default!;
        public UserIdentityMenager USER_ADMIN { get; private set; } = default!;
        public UserIdentityMenager USER_MEMBER_TIME { get; private set; } = default!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                    services.AddDbContext<CashFlowDbContext>(config =>
                    {
                        config.UseInMemoryDatabase("InMemoryDbForTesting");

                        config.UseInternalServiceProvider(provider);
                    });

                    var scope = services.BuildServiceProvider().CreateScope();

                    var dbcontext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                    var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                    var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                    StartDataBase(dbcontext, passwordEncripter, accessTokenGenerator);


                });

        }



        private void StartDataBase(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator)
        {
            var userTeamMember = AddUserTeamMember(dbContext, passwordEncripter, tokenGenerator);
            var expenseTeamMember = AddExpense(dbContext, userTeamMember, expenseId: 1);

            EXPENSE_MEMBER_TIME = new ExpenseIdentityManegarn(expenseTeamMember);

            var userAdmin = AddUserAdmin(dbContext, passwordEncripter, tokenGenerator);
            var expenseAdmin = AddExpense(dbContext, userAdmin, expenseId: 2);
    
            EXPENSE_ADMIN = new ExpenseIdentityManegarn(expenseAdmin);


            dbContext.SaveChanges();
        }
        private User AddUserTeamMember(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator)
        {

            var user = UserBuilder.Build();
            user.Id = 1;

            var password = user.Password;
            user.Password = passwordEncripter.Encrypt(password);

            dbContext.Users.Add(user);

            var token = tokenGenerator.GenerateToken(user);

            USER_MEMBER_TIME = new UserIdentityMenager(user, password, token);


            return user;
        }
        private User AddUserAdmin(CashFlowDbContext cashFlowDbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator)
        {
            var user = UserBuilder.Build();
            user.Id = 2;
            var password = user.Password;
            user.Password = passwordEncripter.Encrypt(password);

            cashFlowDbContext.Users.Add(user);

            var token = tokenGenerator.GenerateToken(user);

            USER_ADMIN = new UserIdentityMenager(user, password, token);

            return user;

        }
        private static Expenses AddExpense(CashFlowDbContext dbContext, User user, long expenseId)
        {
            var expense = ExpenseBuilder.Build(user);
            expense.Id = expenseId;
            dbContext.Expenses.Add(expense);

            return expense;

        }
    }
}
