using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        bool EmailExists(string email);
        Task<bool> EmailExistsAsync(string email);
        Task AddAsync(User user);
        Task<User?> FindUserByEmailAsync(string email);

    }
}
