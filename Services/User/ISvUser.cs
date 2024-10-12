using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISvUser
    {
        Task<List<User>> GetAllUsersAsync();
        Task<(bool Succeeded, string[] Errors)> RegisterUserAsync(string userName, string password, string role);
    }

}
