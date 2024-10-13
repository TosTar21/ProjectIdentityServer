using Entities;

namespace Services
{
    public interface ISvRole
    {
        Task<List<Role>> GetAllRolesAsync();
    }
}
