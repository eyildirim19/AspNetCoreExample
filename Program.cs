using AspNetCoreExample.Models;
using AspNetCoreExample.Models.Entities;
using Microsoft.AspNetCore.Identity;
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
            builder.Services.AddControllersWithViews() // COntroller yapýsýný viewli oluþtur..; 
                .AddJsonOptions(c => c.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services.AddDbContext<AppDbContext>(c =>
              c.UseSqlServer(builder.Configuration.GetConnectionString("constr"))
                );

            // identity'i ekliyoruz...
            builder.Services.AddIdentity<AppUser, AppRole>(c =>
            {
                c.Password.RequireUppercase = false;
                c.Password.RequireLowercase = false;
                c.Password.RequiredLength = 3;
                c.Password.RequireNonAlphanumeric = false;
                c.User.RequireUniqueEmail = true; // Email adresi unique olsun...
                c.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@/";
            })
            .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddSession(c =>
            {

                // session süreleri'ni çok fazla uzatmayýn...
                c.IdleTimeout = TimeSpan.FromMinutes(20); // session saklama süresi 2 dakika. eðer uygulamada 2 dakikadan fazla aktif deðilseniz veri 2 dakikadan sonra silinir...
            }); // session'ý ekle

            // middleware
            var app = builder.Build();


            // Sabit dosyalara eriþmek için sabit doslarý httprequestlerine açmak gerekir..bunun için useStaticFiles middleware kullanýlýr
            app.UseStaticFiles(); // default solution'daki wwwroot folderini hhtprequest eriþime açar..

            app.UseSession(); // session middlewarei  kullan

            app.UseRouting(); // rota kullan

            app.UseAuthentication(); // oturum
            app.UseAuthorization(); // yetki

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "Ozel",
                    //pattern: "{ProductName}-{ProductId}",
                    pattern: "Urun/{ProductName}-{ProductId}/Detay",
                     defaults: new { controller = "Product", action = "Detay" });
                endpoints.MapControllerRoute(
                  name: "Ozel",
                   //pattern: "{ProductName}-{ProductId}",
                   pattern: "Kategori/{CatId}",
                    defaults: new { controller = "Product", action = "Index" });
                endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );
                endpoints.MapControllerRoute(
                name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            }); // Rota eþleþtir

            //app.UseEndpoints(endpoints =>
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //         pattern: "{controller=Home}/{action=Index}")
            //); // Rota eþleþtir

            //app.UseMvc(); // asp.Net core mvc yapýsýný kullanýyoruz..reuest//response arasýna girerek.
            app.Run();
        }
    }
}