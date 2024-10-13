using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class SvResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly MyContext _context;

    public SvResourceOwnerPasswordValidator(MyContext context)
    {
        _context = context;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        // Buscar el usuario por nombre de usuario
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserName == context.UserName);

        // Si el usuario existe, verificar la contraseña
        if (user != null && VerifyHashedPassword(user.Password, context.Password))
        {
            // Si la contraseña es válida, agregar los claims y validar
            context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: "password",
                claims: new List<Claim> { new Claim("role", user.Role.Name) });
        }
        else
        {
            // Si no, retornar error de credenciales inválidas
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
        }
    }

    // Método para verificar si el hash coincide
    private bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        // Hashear la contraseña proporcionada para comparar con la guardada en la base de datos
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(providedPassword));
            var providedHashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            // Comparar el hash generado con el almacenado en la base de datos
            return providedHashedPassword == hashedPassword;
        }
    }
}
