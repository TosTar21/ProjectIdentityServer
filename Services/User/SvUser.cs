using Entities;
using Microsoft.EntityFrameworkCore;
using Services;
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

    // Registrar un usuario y asignar un rol
    public async Task<(bool Succeeded, string[] Errors)> RegisterUserAsync(string userName, string password, string role)
    {
        // Verificar si el usuario ya existe
        if (await _context.Users.AnyAsync(u => u.UserName == userName))
        {
            return (false, new[] { "El nombre de usuario ya está en uso." });
        }

        // Verificar si el rol existe
        var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
        if (userRole == null)
        {
            return (false, new[] { "El rol especificado no es válido." });
        }

        // Crear el nuevo usuario con la contraseña hasheada
        var user = new User
        {
            UserName = userName,
            Password = HashPassword(password),
            RoleId = userRole.RoleId
        };

        // Agregar el usuario a la base de datos
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, new string[] { });
    }

    //Metodo Get all de users
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync(); 
    }

    //Metodo delte de users
    public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return (false, new[] { "Usuario no encontrado." });
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return (true, new string[] { });
    }

    // Método para hashear la contraseña
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
