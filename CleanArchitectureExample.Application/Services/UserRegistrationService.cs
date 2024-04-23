using CleanArchitectureExample.Application.DTO;
using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Application.Mappers;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool EmailExists(string email)
        {
            return _userRepository.EmailExists(email);
        }

        public bool RegisterUser(string name, string email)
        {
            var userExists = _userRepository.EmailExists(email);
            if(userExists)
            {
                return false;
            }
            var user = new User { Id = Guid.NewGuid(), Name = name, Email = email };
            _userRepository.Add(user);
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<bool> RegisterUserAsync(string name, string email)
        {
            try
            {
                if (await EmailExistsAsync(email))
                {
                    return false;
                }

                var user = new User { Id = Guid.NewGuid(), Name = name, Email = email };
                await _userRepository.AddAsync(user);
                return true;
            } catch (ApplicationException)
            {
                return false;
            }
        }

        public async Task<UserDto?> FindUserByEmailAsync(string email)
        {
            var user = await _userRepository.FindUserByEmailAsync(email);
            return UserMapper.MapToDto(user);
        }
    }
}
