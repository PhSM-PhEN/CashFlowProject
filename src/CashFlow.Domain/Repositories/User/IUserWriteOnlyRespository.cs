namespace CashFlow.Domain.Repositories.User
{
    public interface IUserWriteOnlyRespository
    {
        Task Add(Domain.Entities.User user);
    }
}
