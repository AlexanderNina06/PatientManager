namespace PatientMgmt.Core.Domain;

public class Doctor:AuditableBaseEntity
{
  public string Name { get; set; }  
  public string LastName { get; set; }
  public string Email { get; set; }
  public int Phone { get; set; }
  public int IdCard { get; set; }
  public string? Picture { get; set; }

}
