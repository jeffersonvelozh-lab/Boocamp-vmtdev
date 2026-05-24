using Microsoft.EntityFrameworkCore;
using Steam.Domain.Database.SqlServer.Context;
using Steam.Domain.Database.SqlServer.Entities;
using Steam.Domain.Interfaces.Repositories;

namespace Steam.Infrastructure.Persistence.SqlServer.Repositories
{
    public class UserRepository(ArcadeXContext context) : IUserRepository
    {
        public async Task<User> Create(User user)
        {
            try
            {
                // insert en la base datos
                await context.AddAsync(user);

                // commit en la base de datos
                await context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> Get(Guid userId)
        {
            try
            {
                return await context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Deleteat == null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<User?> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasCreated()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IfExiste(Guid userId)
        {
            try
            {
                return await context.Users.AnyAsync(x => x.Id == userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<User> Queryable()
        {
            try
            {

                return context.Users.Where(x => x.Deleteat == null).AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> Update(User user)
        {
            try
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {

                throw;

            }
        }
    }
}
