using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using RMUI.Models;

namespace RMUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string[] roles = { "Admin", "Manager", "Chef", "Server", "SuperAdmin", "DoNotAllowToBecomeAdmin", "DoNotAllowToBecomeManager", "DoNotAllowToBecomeServer" };

            foreach (var role in roles)
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
        }

        public DbSet<RMDataLibrary.Models.AdsModel> AdsModel { get; set; }

        public DbSet<Models.PermissionSource> PermissionSource { get; set; }

        public DbSet<RMUI.Models.PaymentOptionsModel> PaymentOptionsModel { get; set; }
    }


    public class SeedDbContext
    {
        public static void SeedAdminUser(UserManager<IdentityUser> users)
        {
            var u = users.FindByEmailAsync("abc@xyz.com").Result;

            if (u == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "abc@xyz.com",
                    Email = "abc@xyz.com",
                    NormalizedUserName = "ABC@XYZ.COM",
                    NormalizedEmail = "ABC@XYZ.COM",
                    EmailConfirmed = true
                };


                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "Spaghetti135_");
                user.PasswordHash = hashed;
                var userCreated = users.CreateAsync(user).Result;
                users.AddToRoleAsync(user, "Server").Wait();
                users.AddToRoleAsync(user, "SuperAdmin").Wait();
                users.AddToRoleAsync(user, "Admin").Wait();
                users.AddToRoleAsync(user, "Manager").Wait();
            }
        }
    }

}
