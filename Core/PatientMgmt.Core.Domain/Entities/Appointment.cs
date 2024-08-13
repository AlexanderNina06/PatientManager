using System.Data;

namespace PatientMgmt.Core.Domain;

public class Appointment : AuditableBaseEntity
{
public DateTime AppointmentDateTime { get; set; }
public string Cause {get; set; }
public ICollection<LabTest> tests { get; set; }
public ApptStatus appointmentStatus{ get; set; }

//Navegation Properties
public Doctor doctor{ get; set; }
public int DoctorId { get; set; }
public Patient patient{ get; set; }
public int PatientId { get; set; }
public ICollection<TestResult> TestResults  { get; set; }
public string UserId { get; set; }

}
