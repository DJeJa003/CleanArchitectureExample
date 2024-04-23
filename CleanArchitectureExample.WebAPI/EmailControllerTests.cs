using CleanArchitectureExample.Application.DTO;
using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.WebAPI.Controllers;
using CleanArchitectureExample.WebAPI.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitectureExample.WebAPI
{
    public class EmailControllerTests
    {
        [Fact]
        public async Task FindUserByEmail_ReturnsOk_WhenUserFound()
        {
            var expectedUser = new UserDto { Name = "Test User", Email = "test@example.com" };
            var mockService = new Mock<IUserRegistrationService>();
            mockService.Setup(service => service.FindUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            var controller = new EmailController(mockService.Object);

            var result = await controller.FindUserByEmail("test@example.com");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var userDto = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(expectedUser.Name, userDto.Name);
            Assert.Equal(expectedUser.Email, userDto.Email);
        }

        [Fact]
        public async Task FindUserByEmail_ReturnsBadRequest_WhenUserNotFound()
        {
            var mockService = new Mock<IUserRegistrationService>();
            mockService.Setup(service => service.FindUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((UserDto)null);

            var controller = new EmailController(mockService.Object);

            var result = await controller.FindUserByEmail("nonexistent@example.com");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User not found.", badRequestResult.Value);
        }

    }
}
