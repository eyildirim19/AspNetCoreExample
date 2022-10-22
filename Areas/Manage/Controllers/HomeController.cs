using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Areas.Manage.Controllers
{
    // Arae içeriisndeki controllerlara Arae attribute ile name verilir

    [Area("Manage")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           return View();
        }
    }
}
