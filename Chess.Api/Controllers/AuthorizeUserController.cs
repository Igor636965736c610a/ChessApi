using ChesApi.Infrastructure.DTO;
using ChesApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers
{
    [Route("EnglishApi/account")]
    [Authorize]
    public class AuthorizeUserController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthorizeUserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("user/username")]
        public async Task<UserDto> GetUserByUsername(string userName)
            => await _userService.GetUserByUsername(userName);

        [HttpGet("user/name")]
        public async Task<IEnumerable<UserDto>> GetUsersByName(string name)
            => await _userService.GetUsersByName(name);

        [HttpGet("user/email")]
        public async Task<UserDto> GetUserByEmail(string email)
            => await _userService.GetUserByEmail(email);
    }
}
