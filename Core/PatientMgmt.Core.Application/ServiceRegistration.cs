using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Core.Application.services;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public static class ServiceRegistration
{
public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
{
	services.AddAutoMapper(Assembly.GetExecutingAssembly());

	#region Services
	  services.AddTransient<IDoctorService, DoctorService>();
    services.AddTransient<IPatientService, PatientService>();
    services.AddTransient<ILabTestService, LabTestService>();
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IAppointmentService, AppointmentService>();

    #endregion
}
}
