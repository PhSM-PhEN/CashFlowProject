namespace CashFlow.Domain.Repositories.User
{
    public interface IUserReadOnlyRespository
    {
        Task<bool> ExistsByEmail(string email);
    }
}
