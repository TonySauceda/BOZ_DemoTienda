using Microsoft.AspNetCore.Identity;

namespace DemoTienda.Infraestructure.Auth;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
}