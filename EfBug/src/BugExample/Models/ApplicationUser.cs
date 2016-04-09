using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BugExample.Models
{
	public class ApplicationUser : IdentityUser<string>
	{
		public List<Product> ProductsCreated { get; set; }
		public List<Product> ProductsLastModified { get; set; }
	}
}