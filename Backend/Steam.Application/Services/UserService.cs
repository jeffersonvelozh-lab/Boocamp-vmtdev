using Steam.Application.Helpers;
using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;
using Steam.Application.Models.Responses;
using Steam.Shared.Cache;
using Steam.Shared.Helpers;

namespace Steam.Application.Services
{
    public class UserService(Cache<UserDto> cache) : IUserService

    {
        public GenericResponse<UserDto> Create(CreateUserRequest modl)
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

        public GenericResponse<bool> Delete(Guid UserId)
        {
            var Existe = cache.Get(UserId.ToString());

            if (Existe is null)
            {
                return PesponseHelper.Create(false);
            }

            cache.Delete(UserId.ToString());
            return PesponseHelper.Create(true);
        }

        public GenericResponse<bool> Delete(UpdateUserRequest modl)
        {
            throw new NotImplementedException();
        }

        public GenericResponse<UserDto?> Get(Guid UserId)
        {
            var usuario = cache.Get(UserId.ToString());
            return PesponseHelper.Create(usuario);
        }

        public GenericResponse<List<UserDto>> GetAll(int limit, int offset)
        {
            var usuario = cache.Get();
            return PesponseHelper.Create(usuario);
        }

        public GenericResponse<UserDto> Update(UpdateUserRequest modl)
        {
            throw new NotImplementedException();
        }
    }
}
