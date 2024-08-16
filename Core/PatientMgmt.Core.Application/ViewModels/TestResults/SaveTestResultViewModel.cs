using System.Runtime.CompilerServices;

namespace PatientMgmt.Core.Application;

public class SaveTestResultViewModel
{
  public int Id { get; set; }
  public string? Result { get; set;}
  public string? ResultStatus {get; set;} 
  public int? LabTestId { get; set; }
  public int? AppointmentIdFK { get; set; }
  public string? UserId { get; set; }

}
