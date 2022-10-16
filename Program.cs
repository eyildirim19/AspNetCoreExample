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

            //builder.Services.AddControllers(); // Controller yap�s�n� olu�tur
            builder.Services.AddControllersWithViews() // COntroller yap�s�n� viewli olu�tur..; 
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

                // session s�releri'ni �ok fazla uzatmay�n...
                c.IdleTimeout = TimeSpan.FromMinutes(20); // session saklama s�resi 2 dakika. e�er uygulamada 2 dakikadan fazla aktif de�ilseniz veri 2 dakikadan sonra silinir...
            }); // session'� ekle

            // middleware
            var app = builder.Build();


            // Sabit dosyalara eri�mek i�in sabit doslar� httprequestlerine a�mak gerekir..bunun i�in useStaticFiles middleware kullan�l�r
            app.UseStaticFiles(); // default solution'daki wwwroot folderini hhtprequest eri�ime a�ar..

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
            }); // Rota e�le�tir

            //app.UseEndpoints(endpoints =>
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //         pattern: "{controller=Home}/{action=Index}")
            //); // Rota e�le�tir

            //app.UseMvc(); // asp.Net core mvc yap�s�n� kullan�yoruz..reuest//response aras�na girerek.
            app.Run();
        }
    }
}