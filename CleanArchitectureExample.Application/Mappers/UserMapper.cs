using CleanArchitectureExample.Application.DTO;
using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto MapToDto(User user)
        {
            if (user == null)
            {
                return null;
            }

            else return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
