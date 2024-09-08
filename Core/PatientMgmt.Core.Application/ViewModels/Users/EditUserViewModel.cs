using System;
using System.ComponentModel.DataAnnotations;

namespace PatientMgmt.Core.Application.ViewModels.Users;

public class EditUserViewModel
{

  [Required]
  public string Id { get; set; }
  [Required]
  [Display(Name = "User Name")]
  public string UserName { get; set; }
  [Required]
  [EmailAddress]
  public string Email { get; set; }
  [Display(Name = "First Name")]
  public string? FirstName { get; set; }
  [Display(Name = "Last Name")]
  public string? LastName { get; set; }

}
