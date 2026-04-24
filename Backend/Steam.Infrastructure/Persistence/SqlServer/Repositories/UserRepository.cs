using Steam.Domain.Database.SqlServer.Context;
using Steam.Domain.Database.SqlServer.Entities;
using Steam.Domain.Interfaces.Repositories;

namespace Steam.Infrastructure.Persistence.SqlServer.Repositories
{
    internal class UserRepository(SteamcloneBdContext context) : IUserRepository
    {
        public Task<Usuario> Create(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> Get(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasCreated()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IfExiste(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Usuario> Queryable()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
