using Microsoft.AspNetCore.Http;

namespace PatientMgmt.Core.Application;

public class SaveDoctorViewModel
{
  public int Id { get; set; }
  public string Name { get; set; }  
  public string LastName { get; set; }
  public string Email { get; set; }
  public int Phone { get; set; }
  public int IdCard { get; set; }
  public string? Picture { get; set; }
  public IFormFile PictureFile { get; set; }
}
