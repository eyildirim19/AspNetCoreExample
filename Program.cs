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

            //builder.Services.AddControllers(); // Controller yapýsýný oluþtur
            builder.Services.AddControllersWithViews(); // COntroller yapýsýný viewli oluþtur..

            builder.Services.AddDbContext<AppDbContext>(c =>
              c.UseSqlServer(builder.Configuration.GetConnectionString("constr"))
                );

            // middleware
            var app = builder.Build();


            // Sabit dosyalara eriþmek için sabit doslarý httprequestlerine açmak gerekir..bunun için useStaticFiles middleware kullanýlýr
            app.UseStaticFiles(); // default solution'daki wwwroot folderini dýþarý açar..

            app.UseRouting(); // rota kullan

            app.UseEndpoints(endpoints =>
                endpoints.MapControllerRoute(
                    name: "default",
               pattern: "{controller=Home}/{action=Index}")
            ); // Rota eþleþtir

            //app.UseMvc(); // asp.Net core mvc yapýsýný kullanýyoruz..reuest//response arasýna girerek.
            app.Run();
        }
    }
}