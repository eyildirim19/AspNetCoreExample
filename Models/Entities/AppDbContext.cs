using AspNetCoreExample.Models.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// Entity sınıfın IdentityDbContext sınıfından türemelidir
// Identity tablolarını veritabanına yansıtmak için migration yapılır
// Migration yapabilmek iiçn Microsoft.EntityFrameworkCore.Tools kütüphanesini indirdik

// IdentityUser,IdentityRole sınıfları ve diğer sınıflar almak içinjde 

namespace AspNetCoreExample.Models.Entities
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
		// Conntectionstring Program.cs sınıfında di ile belirleniyor...
		public AppDbContext(DbContextOptions builder)
			: base(builder)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // identity tablolarını veritabanımıza yansıtacağımız için migration yapıyoruz. ancak biz bu iki tablo özelinde migration yapmamıştık. bu yüzden bu  tablolara migrationa dahil ediyor. Bizde diyoruzki bu iki tabloyu migrationa dahil etme. İlgi configuration dosyalarında ExcludeFromMigrations metodu ile bu işi yapıyoruz...
            // migration'a dahil etme..
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //	optionsBuilder.UseSqlServer(@"Server=.\mssqlexpress;Database=Northwind;uid=sa;pwd=123");
        //}

        public DbSet<Categories> Categories { get; set; }
		public DbSet<Products> Products { get; set; }
	}



}
