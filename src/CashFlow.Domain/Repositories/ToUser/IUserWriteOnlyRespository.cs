namespace CashFlow.Domain.Repositories.ToUser
{
    public interface IUserWriteOnlyRespository
    {
        Task Add(Entities.User user);

        Task Delete(Entities.User user);
    }
}
