using System;

namespace PatientMgmt.Core.Application.ViewModels.TestResults;

public class PendingTestResultViewModel
{
public int Id { get; set; }
public string  PatientName { get; set; }
public string ResultStatus { get; set; }
public int LabTestId { get; set; }
public int AppointmentId { get; set; }
public string TestName { get; set; }
public int IdCard { get; set; }
}
