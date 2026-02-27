using CashFlow.Domain.Repositories.ToUser;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories.User
{
    internal class UserRespository(CashFlowDbContext dbContext) : IUserReadOnlyRespository , IUserWriteOnlyRespository, IUserUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext = dbContext;

        public async Task Add(Domain.Entities.User user)
        {
             await _dbContext.Users.AddAsync(user);
        }

        public async Task Delete(Domain.Entities.User user)
        {
             var userToRemove = await _dbContext.Users.FindAsync(user.Id);
            _dbContext.Users.Remove(userToRemove!);
        }

        public async Task<bool> ExistByEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<Domain.Entities.User> GetById(long id)
        {
            return await _dbContext.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<Domain.Entities.User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public void Update(Domain.Entities.User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
