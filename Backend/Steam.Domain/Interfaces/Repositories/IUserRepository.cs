using Steam.Domain.Database.SqlServer.Entities;

namespace Steam.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<bool> Delete(User user);
        Task<User> Update(User user);
        Task<User?> Get(int userId);
        Task<User?> Get(String email);
        Task<bool> IfExiste(int userId);
        IQueryable<User> Queryable();
        Task<bool> HasCreated();
    }
}
