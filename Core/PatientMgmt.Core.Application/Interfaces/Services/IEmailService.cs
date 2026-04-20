using System;
using PatientMgmt.Core.Application.DTOs.Email;

namespace PatientMgmt.Core.Application.Interfaces.Services;

public interface IEmailService
{
  Task SendAsync(EmailRequest request);
}
