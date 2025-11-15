using Microsoft.AspNetCore.Identity;

namespace DemoTienda.Infraestructure.Auth;

public static class IdentitySeeder
{
    public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        // Seed Default Admin User
        string adminUserName = "TonySauceda";
        var adminUser = await userManager.FindByNameAsync(adminUserName);
        if (adminUser == null)
        {
            adminUser = new AppUser()
            {
                UserName = adminUserName,
                FullName = "Tony Sauceda",
            };

            var result = await userManager.CreateAsync(adminUser, "C@X@7$aT5PsZz$hpED@b");

            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}