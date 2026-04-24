using Steam.Domain.Database.SqlServer.Entities;

namespace Steam.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Usuario> Create(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task<Usuario?> Get(Guid usuarioId);
        Task<Usuario?> Get(String email);
        Task<bool> IfExiste(Guid usuarioId);
        IQueryable<Usuario> Queryable();
        Task<bool> HasCreated();
    }
}
