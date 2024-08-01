using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public class SaveAppointmentViewModel
{
public int Id { get; set; }
public string  PatientName { get; set; }
public string  DcotorName { get; set; }
public DateTime AppointmentDateTime { get; set; }
public string Cause {get; set; }
public ICollection<LabTest> tests { get; set; }

}
