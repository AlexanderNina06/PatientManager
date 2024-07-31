using Microsoft.AspNetCore.Identity;
using OnlineStoreMS.Core.Application.Enums;
using OnlineStoreMS.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreMS.Infrastructure.Identity.Seeds
{
	public class DefaultBasicUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultUser = new();
			defaultUser.UserName = "basicUser";
			defaultUser.Email = "fakeEmail@email.com";
			defaultUser.FirstName = "John";
			defaultUser.LastName = "Jones";
			defaultUser.EmailConfirmed = true;
			defaultUser.PhoneNumberConfirmed = true;

			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "123Pa$$word!");
					await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
				}

			}
		}
	}
}
