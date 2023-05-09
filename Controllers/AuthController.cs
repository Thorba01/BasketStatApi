using BasketStatsApi.Data;
using BasketStatsApi.Dtos.User;
using BasketStatsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasketStatsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User { Username = request.username }, request.password
                );
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(request.username, request.password);
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
    }
}
