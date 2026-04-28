using Microsoft.EntityFrameworkCore;
using Steam.Domain.Database.SqlServer.Context;
using Steam.Domain.Database.SqlServer.Entities;
using Steam.Domain.Interfaces.Repositories;

namespace Steam.Infrastructure.Persistence.SqlServer.Repositories
{
    public class UserRepository(SteamCloneContext context) : IUserRepository
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

        public async Task<bool> Delete(User user)
        {
            try
            {
                context.Users.Remove(user);

                var deletecount = await context.SaveChangesAsync();

                return deletecount > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User?> Get(int userId)
        {
            try
            {
                return await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
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

        public async Task<bool> IfExiste(int userId)
        {
            try
            {
                return await context.Users.AnyAsync(x => x.UserId == userId);
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

                return context.Users.AsQueryable();

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
