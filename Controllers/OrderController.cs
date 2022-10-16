using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Controllers
{

    [Authorize(Roles ="uye")] // Authorize attribute ile bu controller'a sadece üyeler gelebilir. 
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
