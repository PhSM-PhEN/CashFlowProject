namespace CashFlow.Domain.Repositories.ToUser
{
    public interface IUserReadOnlyRespository
    {
        Task<bool> ExistByEmail(string email);

        Task<Entities.User?>GetUserByEmail(string email);
    }
}
