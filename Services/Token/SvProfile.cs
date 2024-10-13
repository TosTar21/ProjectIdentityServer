
using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.EntityFrameworkCore;

public class SvProfile : IProfileService
{
    private readonly MyContext _context;

    public SvProfile(MyContext context)
    {
        _context = context;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        // Buscar el claim 'sub' que representa el ID del usuario
        var userIdClaim = context.Subject.FindFirst(ClaimTypes.NameIdentifier) ?? context.Subject.FindFirst("sub");

        if (userIdClaim == null)
        {
            // Si no se encuentra el claim, salir del método
            return;
        }

        // Asegurarse de que el ID del usuario sea un número válido
        if (int.TryParse(userIdClaim.Value, out int userId))
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null && user.Role != null)
            {
                // Agregamos el rol del usuario a los claims del token
                var roleClaim = new Claim("role", user.Role.Name);
                context.IssuedClaims.Add(roleClaim);
            }
        }
    }



    public async Task IsActiveAsync(IsActiveContext context)
    {
        // Buscar el claim 'sub' que representa el ID del usuario
        var userIdClaim = context.Subject.FindFirst(ClaimTypes.NameIdentifier) ?? context.Subject.FindFirst("sub");

        if (userIdClaim == null)
        {
            // Si no se encuentra el claim, el usuario no está activo
            context.IsActive = false;
            return;
        }

        // Asegurarse de que el ID del usuario sea un número válido
        if (int.TryParse(userIdClaim.Value, out int userId))
        {
            var user = await _context.Users.FindAsync(userId);
            context.IsActive = user != null;
        }
        else
        {
            context.IsActive = false;
        }
    }

}
