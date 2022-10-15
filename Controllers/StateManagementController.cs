using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Controllers
{
    public class StateManagementController : Controller
    {
        int sayac = 0;
        public IActionResult Index()
        {
            return View(sayac);
        }

        public IActionResult Arttir()
        {
            // Requesste session kullanmak için session middleware'i aktif etmek gerekir...bunun için program.cs dosyasına gidilir. usesession komutu çağrılır...


            // veri sessionda saklanacak.. öncelikle bakalım sessionda veri var mı?...
            // sessiondan veriler get.. metodu ile okunurken, session'a yazılacak veriler set.. metodu ile yazılır
            if (HttpContext.Session.GetInt32("counter") != null)
            {
                // GetInt32 =>int tipindeki verileri döner. nullable int tipinde olduğu için atamya çalıştığınız tipte nullable olsun...

                // veriyi al..
                sayac = (int)HttpContext.Session.GetInt32("counter"); // veriyi sayac değişkenine ata...
            }

            sayac++;

            // veriyi session'a yaz...
            HttpContext.Session.SetInt32("counter", sayac);

            return View("Index", sayac);
        }

        public IActionResult SessionSil()
        {
            HttpContext.Session.Remove("counter");// session'ı siler
            HttpContext.Session.Clear(); // bütün sessionları siler...
            return View("Index");
        }


        public IActionResult ArttirWithAjax()
        {
            // Cookie var mı?????
            // HttpContext nesnesi requesi ifade eder...
            // cookie'de veri strin olarak saklanır
            string veri = null;
            HttpContext.Request.Cookies.TryGetValue("kuki", out veri); // kuki'den veriyi al...
            if (veri != null)
            {
                sayac = Convert.ToInt32(veri);
            }

            sayac++;
            CookieOptions options = new CookieOptions();
            options.HttpOnly = true; // Sadece http requestlerden erişilsin...
            options.Secure = true; // önerilir...Eğer SameSite kullanmıyorsanız Secure özeliği önerilir..
            options.SameSite = SameSiteMode.Strict;
            options.Expires = DateTime.Now.AddMinutes(5); // kuki'nin expire süresi 5 dakika
                                                          // veriyi kukiye yazalım...

            // Defualt süresi => tarayıcı açık olduğu (session) süredir 
            HttpContext.Response.Cookies.Append("kuki", sayac.ToString(), options);
            return Json(sayac);
        }

        public IActionResult CookieSil()
		{
            HttpContext.Response.Cookies.Delete("kuki"); // kuki'yi sil
            return View("Index");
		}
    }
}