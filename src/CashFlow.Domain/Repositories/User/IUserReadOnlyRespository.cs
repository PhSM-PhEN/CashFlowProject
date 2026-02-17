namespace CashFlow.Domain.Repositories.User
{
    public interface IUserReadOnlyRespository
    {
        Task<bool> ExistByEmail(string email);

        Task<Entities.User?>GetUserByEmail(string email);
    }
}
