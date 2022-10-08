using AspNetCoreExample.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreExample
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // container servis
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddMvc();

            //builder.Services.AddControllers(); // Controller yap�s�n� olu�tur
            builder.Services.AddControllersWithViews(); // COntroller yap�s�n� viewli olu�tur..

            builder.Services.AddDbContext<AppDbContext>(c =>
              c.UseSqlServer(builder.Configuration.GetConnectionString("constr"))
                );

            // middleware
            var app = builder.Build();


            // Sabit dosyalara eri�mek i�in sabit doslar� httprequestlerine a�mak gerekir..bunun i�in useStaticFiles middleware kullan�l�r
            app.UseStaticFiles(); // default solution'daki wwwroot folderini d��ar� a�ar..

            app.UseRouting(); // rota kullan

            app.UseEndpoints(endpoints =>
                endpoints.MapControllerRoute(
                    name: "default",
               pattern: "{controller=Home}/{action=Index}")
            ); // Rota e�le�tir

            //app.UseMvc(); // asp.Net core mvc yap�s�n� kullan�yoruz..reuest//response aras�na girerek.
            app.Run();
        }
    }
}