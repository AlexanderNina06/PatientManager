namespace PatientMgmt.Core.Domain;

public class Patient:AuditableBaseEntity
{
public string Name { get; set; } 
public string LastName { get; set; } 
public int Phone { get; set; } 
public string Direction { get; set; } 
public int IdCard { get; set; } 
public DateTime DateOfBirth { get; set; } 
public bool isSmoker { get; set; } 
public bool hasAllergies { get; set; } 
public string? Picture { get; set; } 

}
