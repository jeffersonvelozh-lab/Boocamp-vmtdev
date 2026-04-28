using Steam.Application.Helpers;
using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;
using Steam.Application.Models.Responses;
using Steam.Domain.Interfaces.Repositories;
using Steam.Shared.Helpers;

namespace Steam.Application.Services
{
    public class UserService(IUserRepository repository) : IUserService

    {
        public async Task<GenericResponse<UserDto>> Create(CreateUserRequest modl)
        {
            var User = new UserDto()
            {
                UserId = Guid.NewGuid(),
                UserName = modl.Nombre,
                Correo = modl.Correo,
                Password = modl.Password,
                Country = modl.Pais,
                LoginDate = DataTimeHelpers.UtcNow(),
                LastLogin = DataTimeHelpers.UtcNow(),
            };

            cache.Add(User.UserId.ToString(), User);

            return PesponseHelper.Create(User);
        }

        public Task CreateFirstUser()
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<bool>> Delete(Guid UserId)
        {
            var Existe = cache.Get(UserId.ToString());

            if (Existe is null)
            {
                return PesponseHelper.Create(false);
            }

            cache.Delete(UserId.ToString());
            return PesponseHelper.Create(true);
        }

        public async Task<GenericResponse<UserDto?>> Get(Guid UserId)
        {
            var usuario = cache.Get(UserId.ToString());
            return PesponseHelper.Create(usuario);
        }

        public GenericResponse<List<UserDto>> Get(FilterUserRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<UserDto>> Update(Guid UserId, UpdateUserRequest modl)
        {
            throw new NotImplementedException();
        }
    }
}
