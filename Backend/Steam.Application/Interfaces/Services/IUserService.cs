using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;
using Steam.Application.Models.Responses;

namespace Steam.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<GenericResponse<UserDto>> Create(CreateUserRequest modl);
        public Task<GenericResponse<UserDto>> Update(Guid UserId, UpdateUserRequest modl);
        public Task<GenericResponse<UserDto?>> Get(Guid UserId);
        public GenericResponse<List<UserDto>> Get(FilterUserRequest model);
        public Task<GenericResponse<bool>> Delete(Guid UserId);
        public Task CreateFirstUser();

    }

}
