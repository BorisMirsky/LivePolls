using Microsoft.AspNetCore.Mvc;
using LivePolls.Domain.Abstractions;

namespace LivePolls.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register([FromBody] string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest(new { message = "Имя пользователя не может быть пустым" });

            var user = await _usersService.GetOrCreateUserAsync(userName);
            return Ok(user.Id);
        }
    }
}