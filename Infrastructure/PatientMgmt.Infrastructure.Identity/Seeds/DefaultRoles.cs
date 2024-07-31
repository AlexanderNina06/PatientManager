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
	public static class DefaultRoles
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));

			await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

			await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

		}
	}
}
