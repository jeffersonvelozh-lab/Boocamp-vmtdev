using Microsoft.EntityFrameworkCore;
using Steam.Domain.Database.SqlServer.Context;
using Steam.Domain.Database.SqlServer.Entities;
using Steam.Domain.Interfaces.Repositories;

namespace Steam.Infrastructure.Persistence.SqlServer.Repositories
{
    public class RolesRepository(ArcadeXContext context) : IRolesRepository
    {
        //Método que inserta en la base de datos
        public async Task<Role> Create(Role role)
        {
            try
            {
                //insert en la base de datos 
                await context.AddAsync(role);

                //commit en la base de datos
                await context.SaveChangesAsync();

                return role;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Role?> Get(int roleId)
        {
            try
            {
                return await context.Roles.FirstOrDefaultAsync(x => x.Id == roleId && x.Deleteat == null);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> IfExiste(int roleId)
        {
            try
            {
                return await context.Roles.AnyAsync(x => x.Id == roleId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<Role> Queryable()
        {
            try
            {
                return context.Roles.Where(x => x.Deleteat == null).AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
