using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientMgmt.Core.Application;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Infrastructure.Identity.Contexts;
using PatientMgmt.Infrastructure.Identity.Entities;
using PatientMgmt.Infrastructure.Identity.Interfaces;
using PatientMgmt.Infrastructure.Identity.Services;

namespace PatientMgmt.Infrastructure.Identity;

public static class ServiceRegistration
{
public static void AddIdentityInfrastructure(this IServiceCollection services,IConfiguration configuration)
{
    #region Contexts
    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
        services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
    }
    else
    {
        services.AddDbContext<IdentityContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
    }
    #endregion

    #region Identity
    services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
        .AddDefaultTokenProviders();

    services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/User";
        options.AccessDeniedPath = "/User/AccesDenied";
    });

    services.AddAuthentication();

    #endregion

    #region AutoMapper
    #endregion

    #region Service
    
    services.AddTransient<IAccountService, AccountService>();
    services.AddTransient<IInfrastructureUserService, UserService>();

    #endregion


}
}
