using Microsoft.AspNetCore.Identity;
using OnlineStoreMS.Core.Application.Enums;
using OnlineStoreMS.Infrastructure.Identity.Entities;

namespace OnlineStoreMS.Infrastructure.Identity.Seeds
{
	public static class DefaultSuperAdminUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultUser = new();
			defaultUser.UserName = "SuperAdminUser";
			defaultUser.Email = "SuperAdminUser@email.com";
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
					await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
				}

			}
		}
	}
}
