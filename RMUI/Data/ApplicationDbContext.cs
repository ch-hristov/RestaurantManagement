using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RMUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
            this.Database.Migrate();
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
    }
}
