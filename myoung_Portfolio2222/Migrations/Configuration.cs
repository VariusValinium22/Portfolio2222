namespace myoung_Portfolio2222.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using myoung_Portfolio2222.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<myoung_Portfolio2222.Models.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			var roleManager = new RoleManager<IdentityRole>(
				new RoleStore<IdentityRole>(context));
			if (!context.Roles.Any(r => r.Name == "Admin"))
			{
				roleManager.Create(new IdentityRole { Name = "Admin" });
			}
			if (!context.Roles.Any(r => r.Name == "Moderator"))
			{
				roleManager.Create(new IdentityRole { Name = "Moderator" });
			}

			var userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(context));
			if (!context.Users.Any(u => u.Email == "martingyoung22@gmail.com"))     //All this code(seed method) is to configuare the program we are writing
			{                                                                       //to pre-existing external frameworks that will take our input DATA
				userManager.Create(new ApplicationUser                              //and set it to pre-defined web structures like tables.
				{
					UserName = "martingyoung22@gmail.com",
					Email = "martingyoung22@gmail.com",
					FirstName = "Martin",
					LastName = "Young",
				}, "Programmer22");
			}
			if (!context.Users.Any(u => u.Email == "ewatkins@coderfoundry.com"))     //All this code(seed method) is to configuare the program we are writing
			{                                                                       //to pre-existing external frameworks that will take our input DATA
				userManager.Create(new ApplicationUser                              //and set it to pre-defined web structures like tables.
				{
					UserName = "ewatkins@coderfoundry.com",
					Email = "ewatkins@coderfoundry.com",
					FirstName = "Eric",
					LastName = "Watkins",
				}, "Abc@123");
			}
			if (!context.Users.Any(u => u.Email == "araynor@coderfoundry.com"))     //All this code(seed method) is to configuare the program we are writing
			{                                                                       //to pre-existing external frameworks that will take our input DATA
				userManager.Create(new ApplicationUser                              //and set it to pre-defined web structures like tables.
				{
					UserName = "araynor@coderfoundry.com",
					Email = "araynor@coderfoundry.com",
					FirstName = "Antonio",
					LastName = "Raynor",
				}, "Abc@123");
			}

			var userId = userManager.FindByEmail("martingyoung22@gmail.com").Id;
			userManager.AddToRole(userId, "Admin");
			var userId1 = userManager.FindByEmail("ewatkins@coderfoundry.com").Id;
			userManager.AddToRole(userId1, "Moderator");
			var userId2 = userManager.FindByEmail("araynor@coderfoundry.com").Id;
			userManager.AddToRole(userId2, "Moderator");
		}
	}
}
