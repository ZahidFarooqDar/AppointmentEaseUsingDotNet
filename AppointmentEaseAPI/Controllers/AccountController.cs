using AppointmentEaseAPI.Process;
using AppointmentEaseAPI.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentEaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var result = await _accountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok("Sign Up Successful....");
            }

            foreach (var error in result.Errors)
            {
                if (error.Code == "UserAlreadyExists")
                {
                    // User already exists, return a custom message
                    return BadRequest(new { Message = error.Description, Action = "Sign In Instead" });
                }
            }

            // Handle other validation errors
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result); //Returns token here
        }
    }
}
