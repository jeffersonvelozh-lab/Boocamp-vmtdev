using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;
using Steam.Application.Models.Responses;

namespace Steam.Application.Interfaces.Services
{
    public interface IUserService
    {
        public GenericResponse<UserDto> Create(CreateUserRequest modl);
        public GenericResponse<UserDto> Update(UpdateUserRequest modl);
        public GenericResponse<bool> Delete(UpdateUserRequest modl);
        public GenericResponse<UserDto?> Get(Guid UserId);
        public GenericResponse<List<UserDto>> GetAll(int limit, int offset);

    }

}
