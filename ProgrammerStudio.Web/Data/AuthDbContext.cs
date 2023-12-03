using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProgrammerStudio.Web.Data;
public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var userId = "37fcd00d-418a-453f-a4cb-e8b271c22d9b"; // thats the normal user id (guid format)
        var adminId = "deae99e7-6f6c-45e2-a03e-486ada611e21"; // thats the admin user id (guid format)
        var superAdminId = "05990ca4-3541-4d69-abe3-192e46d2cfa8"; // thats the super admiun user id (guid format)

        var roles = new List<IdentityRole>
        {
            // +++++ creating each type of user ++++++
            new IdentityRole()
            {
                Name = "admin",
                NormalizedName = "admin",
                Id = adminId,
                ConcurrencyStamp = adminId
            },
            new IdentityRole()
            {
                Name = "superAdmin",
                NormalizedName = "superAdmin",
                Id = superAdminId,
                ConcurrencyStamp = superAdminId
            },
            new IdentityRole()
            {
                Name = "user",
                NormalizedName = "user",
                Id = userId,
                ConcurrencyStamp = userId
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);

        // the super admin creation
        var superAdminUser = new IdentityUser
        {
            UserName = "superadmin",
            Email = "superadmin@email.com",
            NormalizedEmail = "superadmin@email.com".ToUpper(),
            NormalizedUserName = "superadmin".ToUpper(),
            Id = superAdminId
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123"); 

        builder.Entity<IdentityUser>().HasData(superAdminUser);

        // super admin has all other types of users auths....
        var superAdminRoles = new List<IdentityUserRole<string>>()
        {
            new IdentityUserRole<string>
            {
                RoleId = superAdminId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = adminId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = userId,
                UserId = superAdminId
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}
