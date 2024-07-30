using System.Data;

namespace PatientMgmt.Core.Domain;

public class Appointment
{
public int DoctorId { get; set; }

//Navegation Property
public Doctor doctor{ get; set; }
public int PatientId { get; set; }

//Navegation Property
public Patient patient{ get; set; }
public DateTime AppointmentDateTime { get; set; }
public string Cause {get; set; }

}
