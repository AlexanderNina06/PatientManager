namespace PatientMgmt.Core.Domain;

public class TestResult : AuditableBaseEntity
{
public string Result { get; set; }
public ApptAndResultStatus ResultStatus { get; set; }

//Navegation Properties
public int PatientId { get; set;}
public Patient patient { get; set; }
public int AppointmentId { get; set; }
public Appointment Appointment { get; set; }

}
