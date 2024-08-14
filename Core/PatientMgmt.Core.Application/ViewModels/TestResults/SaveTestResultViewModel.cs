using System.Runtime.CompilerServices;

namespace PatientMgmt.Core.Application;

public class SaveTestResultViewModel
{
  public int Id { get; set; }
  public string? Result { get; set;}
  public string ResultStatus {get; set;} 
  public int PatientId { get; set;}
  public int AppointmentFK { get; set; }
  public string? UserId { get; set; }
  public List<AppointmentViewModel>? Appointments{ get; set; }
  public List<PatientViewModel>? Patients{ get; set; }

}
