﻿using Microsoft.AspNetCore.Identity;


namespace PatientMgmt.Infrastructure.Identity.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? UserType { get; set; }
	}
}
