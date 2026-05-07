using Steam.Application.Helpers;
using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;
using Steam.Application.Models.Responses;
using Steam.Domain.Database.SqlServer.Entities;
using Steam.Domain.Interfaces.Repositories;
using Steam.Shared.Constans;

namespace Steam.Application.Services
{
    public class UserService(IUserRepository repository) : IUserService

    {
        public async Task<GenericResponse<UserDto>> Create(CreateUserRequest modl)
        {
            var create = await repository.Create(new User
            {
                Username = modl.Nombre,
                Email = modl.Correo,
                Passwordhash = modl.Password,
                Country = modl.Pais

            });

            return PesponseHelper.Create(Map(create));
        }

        public Task CreateFirstUser()
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<bool>> Delete(Guid UserId)
        {
            var user = await GetUser(UserId);

            var delete = await repository.Delete(user);

            return PesponseHelper.Create(delete);
        }

        public async Task<GenericResponse<UserDto>> Get(Guid UserId)
        {
            var user = await GetUser(UserId);
            return PesponseHelper.Create(Map(user));
        }

        public GenericResponse<List<UserDto>> Get(FilterUserRequest model)
        {
            var querable = repository.Queryable();

            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                querable = querable.Where(x => x.Username.Contains(model.Nombre ?? ""));
            }
            if (string.IsNullOrWhiteSpace(model.Correo))
            {
                querable = querable.Where(x => x.Email.Contains(model.Correo ?? ""));
            }
            if (string.IsNullOrWhiteSpace(model.Pais))
            {
                querable = querable.Where(x => x.Country != null && x.Country.Contains(model.Pais ?? ""));
            }

            var users = querable.Take(model.Limit).Skip(model.Offset).ToList();

            List<UserDto> mapped = [];
            foreach (var user in users)
            {
                mapped.Add(Map(user));
            }

            return PesponseHelper.Create(mapped);
        }

        public async Task<GenericResponse<UserDto>> Update(Guid UserId, UpdateUserRequest modl)
        {
            var user = await GetUser(UserId);

            user.Username = modl.Nombre ?? user.Username;
            user.Email = modl.Correo ?? user.Email;
            user.Passwordhash = modl.Password ?? user.Passwordhash;
            user.Country = modl.Pais ?? user.Country;

            var update = await repository.Update(user);

            return PesponseHelper.Create(Map(update));
        }


        //Método que sirve para validar si el usuario existe
        private async Task<User> GetUser(Guid userId)
        {
            return await repository.Get(userId)
                ?? throw new Exception(ResponseConstans.USER_NOT_EXISTS);
        }

        private static UserDto Map(User user)
        {
            return new UserDto
            {
                UserId = user.Id,
                UserName = user.Username,
                Correo = user.Email,
                Password = user.Passwordhash,
                Country = user.Country,
                CreateAt = user.Createdat,
                LastLogin = user.Lastlogin

            };
        }
    }
}
