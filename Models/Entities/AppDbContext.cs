﻿using Microsoft.EntityFrameworkCore;



// Microsoft.AspNetCore.Identity.EntityFrameworkCore =>identity tablolarını kullanmak için öncelikle bu paketi indirmemiz gerekiyor...

// Entity sınıfın IdentityDbContext sınıfından türemelidir

namespace AspNetCoreExample.Models.Entities
{
    public class AppDbContext : DbContext
    {
		// Conntectionstring Program.cs sınıfında di ile belirleniyor...
		public AppDbContext(DbContextOptions builder)
			: base(builder)
		{
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer(@"Server=.\mssqlexpress;Database=Northwind;uid=sa;pwd=123");
		//}

		public DbSet<Categories> Categories { get; set; }
		public DbSet<Products> Products { get; set; }
	}
}
