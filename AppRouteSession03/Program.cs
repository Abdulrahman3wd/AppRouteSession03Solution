

using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AppRouteSession03.PL.Helpers;
using System;
using Microsoft.Extensions.Configuration;
using AppRouteSession03.PL.Extentions;
using Microsoft.Extensions.Hosting;

namespace AppRouteSession03
{
    public class Program
    {
       public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services
			webApplicationBuilder.Services.AddControllersWithViews();



			///webApplicationBuilder.Services.AddControllers();// API
			///webApplicationBuilder.Services.AddRazorPages();// RazorPages
			///webApplicationBuilder.Services.AddMvc();// MVC            

			///webApplicationBuilder.Services.AddTransient<ApplecationDbContext>();
			/// webApplicationBuilder.Services.AddSingleton<ApplecationDbContext>();


			///webApplicationBuilder.Services.AddScoped<ApplecationDbContext>();
			///webApplicationBuilder.Services.AddScoped<DbContextOptions<ApplecationDbContext>>();
			webApplicationBuilder.Services.AddDbContext<ApplecationDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			}, ServiceLifetime.Scoped);

			object value = webApplicationBuilder.Services.AppApplicationServices(); // Extention Method
			webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
			webApplicationBuilder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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
				.AddEntityFrameworkStores<ApplecationDbContext>()
				.AddDefaultTokenProviders();

			// webApplicationBuilder.Services.AddAuthentication();
			webApplicationBuilder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.AccessDeniedPath = "/Home/Error";
			});
			//webApplicationBuilder.Services.AddAuthentication("Hamada");
			webApplicationBuilder.Services.AddAuthentication(options =>
			{
				//options.DefaultAuthenticateScheme = "Identity.Application";
			})
				.AddCookie("Hamada", options =>
				{
					options.LoginPath = "/Account/SignIn";
					options.ExpireTimeSpan = TimeSpan.FromDays(1);
					options.AccessDeniedPath = "/Home/Error";
				}); 
			#endregion

			var app = webApplicationBuilder.Build();

			#region Configuer Kestrel MiddleWare
			if (app.Environment.IsDevelopment())
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
			#endregion

			app.Run(); // app Ready For Requests


		}
    }
}
