using System;
using System.Data.SqlClient;
using BugExample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BugExample
{
	public class Startup
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddEntityFramework()
				.AddDbContext<ApplicationDbContext>();
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			var serviceProvider = services.BuildServiceProvider();
			using (var sqlConnection = new SqlConnection(
				"Server=.;Integrated Security=true;"))
			{
				sqlConnection.Open();
				foreach (var sql in DbScript.InstantiateSql.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
				{
					new SqlCommand(sql, sqlConnection).ExecuteNonQuery();
				}
			}
			var db = new ApplicationDbContext();
			db.Database.Migrate();
			new Seeder().Seed(
				serviceProvider.GetService<UserManager<ApplicationUser>>()
				);
			return serviceProvider;
		}
	}
}