using Entities;
using Microsoft.EntityFrameworkCore;
using Services.User;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class SvUser : ISvUser
{
    private readonly MyContext _context;

    public SvUser(MyContext context)
    {
        _context = context;
    }

    // Crear un nuevo usuario con un rol basado en RoleId
    public async Task<(bool Succeeded, string[] Errors)> CreateUserAsync(string userName, string email, string password, int roleId)
    {
        // Verificar si el nombre de usuario o el correo ya existen
        if (await _context.Users.AnyAsync(u => u.UserName == userName || u.Email == email))
        {
            return (false, new[] { "El nombre de usuario o el correo electrónico ya están en uso" });
        }

        // Verificar que el rol existe
        var role = await _context.Roles.FindAsync(roleId);
        if (role == null)
        {
            return (false, new[] { "Rol no encontrado" });
        }

        // Crear el usuario con el rol asignado
        var user = new User
        {
            UserName = userName,
            Email = email,
            PasswordHash = HashPassword(password),
            RoleId = role.RoleId // Asignar el RoleId directamente
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, new string[] { });
    }

    // Obtener un usuario por su Id
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.Include(u => u.Role)
                                   .FirstOrDefaultAsync(u => u.Id == userId);
    }

    // Método privado para hashear la contraseña
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
