using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Controllers
{
    public class PermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
