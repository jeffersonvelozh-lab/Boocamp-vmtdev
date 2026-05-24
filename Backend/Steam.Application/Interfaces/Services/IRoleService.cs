using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request.Roles;
using Steam.Application.Models.Responses;

namespace Steam.Application.Interfaces.Services
{
    public interface IRoleService
    {
        public Task<GenericResponse<RoleDto>> Create(CreateRoleRequest model);
        public GenericResponse<List<RoleDto>> Get(FilterRoleRequest model);
    }
}
