using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OtoKiralama.Domain.Enums;
using OtoKiralama.Persistance.Data;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Persistance.Seeding;

public static class SeedIdentityData
{
    public async static Task SeedDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        dbContext.Database.EnsureCreated();
        if (dbContext.Database.GetPendingMigrations().Any()) await dbContext.Database.MigrateAsync();

        // Seed roles
        var roles = Enum.GetNames(typeof(Roles));
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
                if (!result.Succeeded)
                {
                    Console.WriteLine($"Failed to create role {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        // Seed users
        var users = new Dictionary<string, string>
        {
            { "TeymurDevv@gmail.com", "12345@Tt" },
            { "TeymurDevv2@gmail.com", "12345@Tt" },
            { "Nadir810@gmail.com", "12345@Nn" },
            { "Nadir811@gmail.com", "12345@Nn" }
        };

        foreach (var (email, password) in users)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var user = new AppUser
                {
                    FullName = Guid.NewGuid().ToString("N")[..7],
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = null,
                    BirthDate = null,
                    TcKimlik = null,
                };

                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    Console.WriteLine($"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    continue;
                }

                // Retrieve the user again after creation to ensure we get the correct ID
                existingUser = await userManager.FindByEmailAsync(email);
            }

            // Ensure user exists before adding roles
            if (existingUser != null)
            {
                var roleResult = await userManager.AddToRolesAsync(existingUser, roles);
                if (!roleResult.Succeeded)
                {
                    Console.WriteLine($"Failed to assign roles to {email}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}