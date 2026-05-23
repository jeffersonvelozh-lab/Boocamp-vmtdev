using Microsoft.AspNetCore.Mvc;
using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Request;
using Steam.Application.Models.Request.Users;

namespace Steam.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest model)
        {
            var rsv = await userService.Create(model);
            return Ok(rsv);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest model, Guid id)
        {
            var rsv = await userService.Update(id, model);
            return Ok(rsv);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rsv = await userService.Delete(id);
            return Ok(rsv);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterUserRequest model)
        {
            var srv = userService.Get(model);
            return Ok(srv);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rsv = await userService.Get(id);
            return Ok(rsv);
        }
    }
}
