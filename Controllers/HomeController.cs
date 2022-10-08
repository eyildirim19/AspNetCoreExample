using AspNetCoreExample.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext dbContext;
        public HomeController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IActionResult Index()
        {
            var result = dbContext.Products.Take(10).ToList();
            return View(result);
        }
    }
}
