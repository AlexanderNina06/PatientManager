using Microsoft.AspNetCore.Identity;
using PatientMgmt.Infrastructure.Identity.Entities;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Infrastructure.Identity.Seeds
{
	public static class DefaultAdminUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultUser = new();
			defaultUser.UserName = "AdminUser";
			defaultUser.Email = "Adminemail@email.com";
			defaultUser.FirstName = "Carlos";
			defaultUser.LastName = "Jones";
			defaultUser.EmailConfirmed = true;
			defaultUser.PhoneNumberConfirmed = true;

			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "1234Pa$$word!");
					await userManager.AddToRoleAsync(defaultUser, Roles.Assistant.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
					
				}

			}
		}
	}
}
