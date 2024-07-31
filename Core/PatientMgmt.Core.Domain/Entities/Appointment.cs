using System.Data;

namespace PatientMgmt.Core.Domain;

public class Appointment
{
//Navegation Property
public Doctor doctor{ get; set; }
public int DoctorId { get; set; }

//Navegation Property
public Patient patient{ get; set; }
public int PatientId { get; set; }

//-------------------------------
public DateTime AppointmentDateTime { get; set; }
public string Cause {get; set; }

public ICollection<LabTest> tests { get; set; }
public ApptAndResultStatus appointmentStatus{ get; set; }

}
