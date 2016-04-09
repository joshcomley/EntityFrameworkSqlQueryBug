using System.Data.SqlClient;
using BugExample.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BugExample
{
	public static class Program
	{
		private static ServiceCollection _svc;

		public static void Main(string[] args)
		{			
			_svc = new ServiceCollection();
			new Startup().ConfigureServices(_svc);
			var db = new ApplicationDbContext();
			BadExecution(db);
			GoodExecution(db);
		}

		/// <summary>
		/// One query with a left join
		/// </summary>
		/// <param name="db"></param>
		private static void GoodExecution(ApplicationDbContext db)
		{
			var query = db.Customers.Include(p => p.Products);
			var products = query.ToList();
		}

		/// <summary>
		/// Lots of individual and unfiltered queries
		/// </summary>
		/// <param name="db"></param>
		private static void BadExecution(ApplicationDbContext db)
		{
			var query = db.Customers.Select(p => new
			{
				p.FirstName,
				p.LastName,
				Products = p.Products.Select(x => new
				{
					x.Name,
					x.CreatedByUserId
				})
			});
			var result = query.ToList();
			foreach (var customer in result)
			{
				foreach (var product in customer.Products)
				{
				}
			}
		}
	}
}