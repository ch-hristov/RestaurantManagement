using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RMDataLibrary.DataAccess;
using RMUI.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace RMUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => opts.ResourcesPath = "Resources")
                    .AddDataAnnotationsLocalization();
            services.AddRazorPages();

            services.AddLocalization(opts => opts.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("es"),
                    new CultureInfo("fr"),
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

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

                _ = userManager.CreateAsync(user, "Spaghetti135_").Result;
                u = userManager.FindByEmailAsync("abc@xyz.com").Result;
            }

            var token = userManager.GenerateEmailConfirmationTokenAsync(u).Result;

            userManager.ConfirmEmailAsync(u, token).Wait();
            userManager.AddToRoleAsync(u, "Admin").Wait();
            userManager.AddToRoleAsync(u, "Manager").Wait();
            userManager.AddToRoleAsync(u, "Server").Wait();
            userManager.AddToRoleAsync(u, "SuperAdmin").Wait();
        }

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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

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
