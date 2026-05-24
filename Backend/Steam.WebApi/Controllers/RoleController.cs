using Microsoft.AspNetCore.Mvc;
using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Request.Roles;

namespace Steam.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest model)
        {
            var rsv = await roleService.Create(model);
            return Ok(rsv);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterRoleRequest model)
        {
            var srv = roleService.Get(model);
            return Ok(srv);
        }
    }
}
