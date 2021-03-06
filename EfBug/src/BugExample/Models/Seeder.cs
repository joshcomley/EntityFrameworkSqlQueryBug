using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BugExample.Models
{
	public class Seeder
	{
		private CrudBase<Product, int> _productsCrud;
		private int _productId;
		private CrudBase<Order, string> _ordersCrud;

		public void Seed(UserManager<ApplicationUser> userManager)
		{
			//var user = await userManager.FindByIdAsync("1");
			//if (user == null)
			//{
			//	await userManager.CreateAsync(new ApplicationUser()
			//	{
			//		Id = "1",
			//		UserName = "test@example.com",
			//		Email = "test@example.com"
			//	});
			//}
			using (var context = new ApplicationDbContext())
			{
				context.Database.EnsureCreated();
				_productsCrud = new CrudBase<Product, int>(
					context, context.Products, p => p.ProductId);
				_ordersCrud = new CrudBase<Order, string>(
					context, context.Orders, p => p.Id);
				var customersCrud = new CrudBase<Customer, int>(
					context, context.Customers, p => p.CustomerId);
				var currentCustomerId = 0;
				Action<string, string> cust = (firstName, lastName) =>
				{
					customersCrud.EnsureEntity(
						++currentCustomerId, customer =>
						{
							customer.FirstName = firstName;
							customer.LastName = lastName;
						});
				};

				cust("Harry", "Whitburn");
				cust("Nick", "Lawden");
				cust("Emil", "Roijer");
				context.SaveChanges();
				Prod("Apple number1", 10, null, null);
				Prod("Apple number1", 10, 1, null, null);
				Prod("Orange number1", 20, null, new DateTime(2015, 12, 1));
				Prod("Peanut butter number1", 25, 2, null);
				Prod("xApple number2", 10, 1, null);
				Prod("xOrange number2", 20, 2, null);
				Prod("xPeanut butter number2", 25, 2, null);
				Prod("xApple number2", 10, 1, null);
				Prod("xOrange number2", 20, 2, null);
				Prod("xPeanut butter number2", 25, 2, null);
				Prod("xApple number2", 10, 1, null);
				Prod("xOrange number2", 20, 2, null);
				Prod("xPeanut butter number2", 25, 2, null);
				Prod("xApple number2", 10, 1, null);
				Prod("xOrange number2", 20, 2, null);
				Prod("xPeanut butter number2", 25, 2, null);
				Prod("Apple number3", 10, 1, null);
				Prod("Orange number3", 20, 2, null);
				Prod("Peanut butter number3", 25, 2, null);
				Prod("Apple number4", 10, 1, null);
				Prod("Orange number4", 20, 2, null);
				Prod("Peanut butter number4", 25, 2, null);
				Prod("Apple number5", 10, 1, null);
				Prod("Orange number5", 20, 2, null);
				Prod("Peanut butter number5", 25, 2, null);
				Prod("Apple number6", 10, 1, null);
				Prod("Orange number6", 20, 2, null);
				Prod("Peanut butter number6", 25, 2, null);
				context.SaveChanges();
				Order("1", "First order", 1);
				Order("2", "Second order", 2);
				Order("3", "Third order", 1);
				context.SaveChanges();
			}
		}

		private int Prod(string name, double price, int? customerId,
			DateTime? dateCreated, string cratedByUserId = null)
		{
			_productsCrud.EnsureEntity(
				++_productId, product =>
				{
					product.Name = name;
					product.Price = price;
					product.CustomerId = customerId;
					product.DateCreated = dateCreated;
					product.CreatedByUserId = cratedByUserId;
				});
			return _productId;
		}

		private void Order(string id, string title, int customerId)
		{
			_ordersCrud.EnsureEntity(
				id, entity =>
				{
					entity.Id = id;
					entity.Title = title;
					entity.CustomerId = customerId;
				});
		}
	}
}