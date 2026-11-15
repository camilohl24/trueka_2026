using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trueka.Entities;

namespace Trueka.Repositories.Data;

public static class DbSeeder
{
    private static readonly string[] CategoryNames =
    {
        "Ropa y calzado", "Electrónica", "Libros", "Hogar",
        "Deportes", "Juguetes", "Instrumentos musicales", "Otros"
    };

    public static async Task SeedAsync(
        IServiceProvider services, string? adminEmail, string? adminPassword)
    {
        var context = services.GetRequiredService<TruekaDbContext>();
        await context.Database.MigrateAsync();

        // 1. Rol Admin
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        // 2. Usuario administrador
        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
        {
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin is null)
            {
                admin = new AppUser
                {
                    FullName = "Administrador",
                    Email = adminEmail,
                    UserName = adminEmail
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        string.Join(" ", result.Errors.Select(e => e.Description)));
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
                await userManager.AddToRoleAsync(admin, "Admin");
        }

        // 3. Categorías iniciales
        foreach (var name in CategoryNames)
        {
            if (!await context.Categories.AnyAsync(c => c.Name == name))
                context.Categories.Add(new Category { Name = name });
        }

        await context.SaveChangesAsync();
    }
}