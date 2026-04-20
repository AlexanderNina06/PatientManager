using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Infrastructure.Shared.Services;
using PatientMgmt.Core.Domain.Settings;

namespace PatientMgmt.Infrastructure.Shared;

public static class ServiceRegistration
{
public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
{
	services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
	services.AddTransient<IEmailService, EmailService>();
}
}
