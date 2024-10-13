using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Roles
{
    public class SvRole: ISvRole
    {
        private readonly MyContext _context;

        public SvRole(MyContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
