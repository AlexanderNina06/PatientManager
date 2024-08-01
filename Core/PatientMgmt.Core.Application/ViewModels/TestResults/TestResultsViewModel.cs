using System.Runtime.CompilerServices;

namespace PatientMgmt.Core.Application;

public class TestResultsViewModel
{
public int Id { get; set; }
public string Result { get; set; }
public string  PatientName { get; set; }
public int IdCard { get; set; } 
public string ResultStatus { get; set; }
public int PatientId { get; set;}
public int AppointmentId { get; set; }

}
