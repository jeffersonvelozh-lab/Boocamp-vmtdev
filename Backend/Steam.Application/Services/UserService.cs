using Steam.Application.Helpers;
using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;
using Steam.Application.Models.Responses;
using Steam.Domain.Database.SqlServer.Entities;
using Steam.Domain.Interfaces.Repositories;

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
                PasswordHash = modl.Password,
                Country = modl.Pais

            });

            return PesponseHelper.Create(Map(create));
        }

        public Task CreateFirstUser()
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<bool>> Delete(int UserId)
        {
            var findUser = await repository.Get(UserId)
                ?? throw new Exception("El usuario no existe");

            var delete = await repository.Delete(findUser);

            return PesponseHelper.Create(delete);
        }

        public async Task<GenericResponse<UserDto?>> Get(int UserId)
        {
            throw new NotImplementedException();
        }

        public GenericResponse<List<UserDto>> Get(FilterUserRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<UserDto>> Update(int UserId, UpdateUserRequest modl)
        {
            throw new NotImplementedException();
        }

        private static UserDto Map(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.Username,
                Correo = user.Email,
                Password = user.PasswordHash,
                Country = user.Country,
                CreateAt = user.CreatedAt,
                LastLogin = user.LastLogin

            };
        }
    }
}
