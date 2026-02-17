using CashFlow.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories.User
{
    internal class UserRespository(CashFlowDbContext dbContext) : IUserReadOnlyRespository , IUserWriteOnlyRespository
    {
        private readonly CashFlowDbContext _dbContext = dbContext;

        public async Task Add(Domain.Entities.User user)
        {
             await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExistByEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<Domain.Entities.User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}
