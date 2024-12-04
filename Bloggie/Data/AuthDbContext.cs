using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Webb.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed roles (User, Admin, SuperAdmin)
            var adminRoleId = "04550ec0-6cc2-417c-bf0e-be0d5e4cf9c7";
            var superAdminRoleId = "872c14aa-f67b-426a-ae0d-d9e5e9aa1d3b";
            var userRoleId = "c040106b-9f39-4565-b434-58144db55cb3";
            var roles = new List<IdentityRole>
            {
                new IdentityRole 
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                },
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId
                },
                 new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
            //Seed SuperAdminUser
            var superAdminId = "79a53136-31be-4d3f-a279-605e3c1e3e90";
            var superAdminUser = new IdentityUser
            {
                UserName= "superadmin@blogie.com",
                Email = "superadmin@blogie.com",
                NormalizedUserName = "superadmin@blogie.com".ToUpper(),
                NormalizedEmail = "superadmin@blogie.com".ToUpper(),
                Id=superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperadminUser1234");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = adminRoleId,
                     UserId = superAdminId
                 },
                  new IdentityUserRole<string>
                  {
                      RoleId = superAdminRoleId,
                      UserId = superAdminId
                  },
            }; 
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
