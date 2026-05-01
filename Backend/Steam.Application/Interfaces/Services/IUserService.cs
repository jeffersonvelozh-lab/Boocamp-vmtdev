using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;
using Steam.Application.Models.Responses;

namespace Steam.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<GenericResponse<UserDto>> Create(CreateUserRequest modl);
        public Task<GenericResponse<UserDto>> Update(int UserId, UpdateUserRequest modl);
        public Task<GenericResponse<UserDto?>> Get(int UserId);
        public GenericResponse<List<UserDto>> Get(FilterUserRequest model);
        public Task<GenericResponse<bool>> Delete(int UserId);
        public Task CreateFirstUser();

    }

}
