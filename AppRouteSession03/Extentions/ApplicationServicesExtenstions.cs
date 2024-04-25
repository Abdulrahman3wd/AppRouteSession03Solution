using AppRouteSession03.BLL;
using AppRouteSession03.BLL.Interfaces;
using AppRouteSession03.BLL.Repostories;
using AppRouteSession03.PL.Services.EmailSender;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace AppRouteSession03.PL.Extentions
{
    public static class ApplicationServicesExtenstions
    {
        public static IServiceCollection AppApplicationServices (this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;

        }
    }
}
