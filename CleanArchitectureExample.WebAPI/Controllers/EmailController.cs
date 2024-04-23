using CleanArchitectureExample.Application.DTO;
using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Application.Mappers;
using CleanArchitectureExample.Application.Services;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public EmailController(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }

        [HttpGet("FindUser")]
        public async Task<IActionResult> FindUserByEmail(string email)
        {
            try
            {
                var user = await _userRegistrationService.FindUserByEmailAsync(email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                return Ok(user);

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
