using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRegistrationService _registrationService;

        public UsersController(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("Register")]
        public IActionResult RegisterUser(string name, string email)
        {
            _registrationService.RegisterUser(name, email);
            return new OkResult();
        }

        [HttpPost("UserAsync")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationRequest request)
        {
            var success = await _registrationService.RegisterUserAsync(request.Name, request.Email);
            if (!success)
            {
                return new BadRequestObjectResult("Rekisteröinti epäonnistui.");
            }

            return new CreatedResult("GetUsers", null);
        }
    }
}
