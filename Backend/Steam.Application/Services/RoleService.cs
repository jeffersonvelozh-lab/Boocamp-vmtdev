using Steam.Application.Helpers;
using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Dtos;
using Steam.Application.Models.Request.Roles;
using Steam.Application.Models.Responses;
using Steam.Domain.Database.SqlServer.Entities;
using Steam.Domain.Interfaces.Repositories;

namespace Steam.Application.Services
{
    public class RoleService(IRolesRepository repository) : IRoleService
    {
        public async Task<GenericResponse<RoleDto>> Create(CreateRoleRequest model)
        {
            var create = await repository.Create(new Role
            {
                Name = model.RoleName
            });

            return PesponseHelper.Create(Map(create));
        }

        public GenericResponse<List<RoleDto>> Get(FilterRoleRequest model)
        {
            var querable = repository.Queryable();

            if (string.IsNullOrWhiteSpace(model.RoleName))
            {
                querable = querable.Where(x => x.Name.Contains(model.RoleName ?? ""));
            }

            //Realiza la paginación
            var roles = querable.Take(model.Limit).Skip(model.Offset).ToList();

            List<RoleDto> result = [];
            foreach (var role in roles)
            {
                result.Add(Map(role));
            }

            return PesponseHelper.Create(result);
        }

        private static RoleDto Map(Role role)
        {
            return new RoleDto { Name = role.Name };
        }
    }
}
