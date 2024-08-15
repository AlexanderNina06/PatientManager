using System;
using System.Runtime.CompilerServices;

namespace PatientMgmt.Core.Application.ViewModels.TestResults;

public class ConsultResultsViewModel
{
public int AppointmentId { get; set; }
public string ResultStatus { get; set; }
public int LabTestId { get; set; }
public string TestName { get; set; }
public string? Results {get; set;} 
public bool IsAppointmentCompleted { get; set; }
}
