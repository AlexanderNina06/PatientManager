using Microsoft.AspNetCore.Identity;
using PatientMgmt.Core.Domain;
using PatientMgmt.Infrastructure.Identity.Entities;


namespace PatientMgmt.Infrastructure.Identity.Seeds
{
	public static class DefaultRoles
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

			await roleManager.CreateAsync(new IdentityRole(Roles.Assistant.ToString()));

		}
	}
}
