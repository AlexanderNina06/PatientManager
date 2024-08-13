using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public class SaveAppointmentViewModel
{
public int Id { get; set; }
public DateTime AppointmentDateTime { get; set; }
public string Cause {get; set; }
public string? UserId { get; set; }
public int DoctorId { get; set; }
public int PatientId { get; set; }
public List<DoctorViewModel>? Doctors { get; set; }
public List<PatientViewModel>? Patients { get; set; }

//public ICollection<LabTest> tests { get; set; }

}
