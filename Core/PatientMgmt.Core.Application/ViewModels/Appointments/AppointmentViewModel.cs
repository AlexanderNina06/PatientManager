
namespace PatientMgmt.Core.Application;

public class AppointmentViewModel
{
public int Id { get; set; }
public string  PatientName { get; set; }
public string  DoctorName { get; set; }
public int DoctorId { get; set; }
public int PatientId { get; set; }
public DateTime AppointmentDateTime { get; set; }
public string Cause {get; set; }
public string appointmentStatus{ get; set; }

//ignore the following in the Mapping
public string TestFinalResults  { get; set; }
public string TestResultState  { get; set; }
public ICollection<string> TestsNames { get; set; }

}
