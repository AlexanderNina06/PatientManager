using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientMgmt.Core.Application;

namespace PatientMgmt.Infrastructure.Persistence;

public static class ServiceRegistration
{
public static void AddPersistenceInfrastructure(this IServiceCollection services,IConfiguration configuration)
{
    #region Contexts
    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
        services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
    }
    else
    {
        services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("Default"),
        m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
    }
    #endregion

    #region Repositories
    services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddTransient<IDoctorRepository, DoctorRepository>();
    services.AddTransient<IPatientRepository, PatientRepository>();
    services.AddTransient<ILabTestRepository, LabTestRepository>();
    #endregion
}
}
