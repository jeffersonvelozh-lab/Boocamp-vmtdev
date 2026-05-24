using Steam.Domain.Database.SqlServer.Entities;

namespace Steam.Domain.Interfaces.Repositories
{
    public interface IRolesRepository
    {
        Task<Role> Create(Role role);
        Task<Role?> Get(int roleId);
        IQueryable<Role> Queryable();
        Task<bool> IfExiste(int roleId);
    }
}
