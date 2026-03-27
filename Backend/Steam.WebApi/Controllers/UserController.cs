using Microsoft.AspNetCore.Mvc;
using Steam.Application.Helpers;
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
            var rap = userService.Create(model);
            return Ok(rap);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest model, Guid id)
        {
            return Ok($"Usuario actualizado: {id} - {model.Nombre}");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok($"Usuario eliminado: {id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromBody] GetAllUserRequest model)
        {
            return Ok(PesponseHelper.Create(userService.GetAll(model.Limit ?? 0, model.Offset ?? 0)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rap = userService.Get(id);
            return Ok(rap);
        }
    }
}
