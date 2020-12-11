using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMDataLibrary.Models;

namespace RMUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            string[] roles = { "Admin", "Manager", "Chef", "Server" };

            foreach (var role in roles)
            {
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
            }
        }

        public DbSet<RMDataLibrary.Models.AdsModel> AdsModel { get; set; }
    }
}
