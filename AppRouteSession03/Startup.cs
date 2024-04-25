using AppRouteSession03.PL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppRouteSession03.BLL.Repostories;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.PL.Extentions;
using AppRouteSession03.PL.Helpers;
using AppRouteSession03.BLL;
using AppRouteSession03.DAL.Models;
using Microsoft.AspNetCore.Identity;
namespace AppRouteSession03
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
            services.AddControllersWithViews();

 
 
            ///services.AddControllers();// API
            ///services.AddRazorPages();// RazorPages
            ///services.AddMvc();// MVC            

            ///services.AddTransient<ApplecationDbContext>();
            /// services.AddSingleton<ApplecationDbContext>();


            ///services.AddScoped<ApplecationDbContext>();
            ///services.AddScoped<DbContextOptions<ApplecationDbContext>>();
            services.AddDbContext<ApplecationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            } , ServiceLifetime.Scoped);

            services.AppApplicationServices(); // Extention Method
            services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                options.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<ApplecationDbContext>();
            //.AddDefaultTokenProviders();

            // services.AddAuthentication();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.AccessDeniedPath = "/Home/Error";
            });
            //services.AddAuthentication("Hamada");
            services.AddAuthentication(options=>
            {
                //options.DefaultAuthenticateScheme = "Identity.Application";
            })
                .AddCookie("Hamada" , options =>
                {
                    options.LoginPath = "/Account/SignIn";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.AccessDeniedPath = "/Home/Error";
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            });
        }
    }
}
