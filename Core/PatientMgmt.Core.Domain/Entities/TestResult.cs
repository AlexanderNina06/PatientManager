using PatientMgmt.Core.Domain.Enums;

namespace PatientMgmt.Core.Domain;

public class TestResult : AuditableBaseEntity
{
public string? Result { get; set; }
public ResultStatus ResultStatus { get; set; }

//Navegation Properties
public LabTest LabTest { get; set; }
public int LabTestId { get; set; }
public int AppointmentIdFK { get; set; }
public Appointment Appointment { get; set; }
public string UserId { get; set; }

}
