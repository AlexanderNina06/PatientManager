using System;

namespace PatientMgmt.Core.Application.DTOs.Email;

public class EmailRequest
{
  public string To { get; set; }
  public string Subject { get; set; }
  public string Body { get; set; }
}
