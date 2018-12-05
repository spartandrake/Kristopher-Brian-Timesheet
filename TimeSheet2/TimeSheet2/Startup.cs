using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeSheet2.EntityFramework;
using TimeSheet2.EF;
using IdentityMVC.Services;
using TimeSheet2.Initializers;

namespace TimeSheet2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)                 .AddRazorPagesOptions(options =>                 {                     options.AllowAreas = true;                     options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");                     options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");                 });              services.ConfigureApplicationCookie(options =>             {                 options.LoginPath = $"/Identity/Account/Login";                 options.LogoutPath = $"/Identity/Account/Logout";                 options.AccessDeniedPath = $"/Identity/Account/AccessDenied";             });
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IDbInit, DbInit>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInit init)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                init.Initialize();
                init.SeedData();

               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
