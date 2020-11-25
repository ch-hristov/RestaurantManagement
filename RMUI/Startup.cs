using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using RMUI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RMDataLibrary.DataAccess;

namespace RMUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IFoodData, FoodData>();
            services.AddTransient<IDiningTableData, DiningTableData>();
            services.AddTransient<IPersonData, PersonData>();
            services.AddTransient<IOrderData, OrderData>();
            services.AddTransient<IBillData, BillData>();
        }


        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            var u = userManager.FindByEmailAsync("abc@xyz.com").Result;
            if (u == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "abc@xyz.com",
                    Email = "abc@xyz.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Spaghetti135_").Result;

                //if (result.Succeeded)
                //{
                    
                //}
            }
            var token = userManager.GenerateEmailConfirmationTokenAsync(u).Result;
            userManager.ConfirmEmailAsync(u, token).Wait();
            userManager.AddToRoleAsync(u, "Admin").Wait();
            userManager.AddToRoleAsync(u, "Manager").Wait();
            userManager.AddToRoleAsync(u, "Server").Wait();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            SeedUsers(userManager);

        }
    }
}
