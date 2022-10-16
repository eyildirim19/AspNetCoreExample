using AspNetCoreExample.Models;
using AspNetCoreExample.Models.Entities;
using AspNetCoreExample.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCoreExample.Controllers
{
    public class SepetController : Controller
    {
        private readonly AppDbContext _dbContext;
        public SepetController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            string? temp = HttpContext.Session.GetString("sepet");
            List<Sepet> sepets;
            if (temp != null)
                sepets = JsonConvert.DeserializeObject<List<Sepet>>(temp);
            else
                sepets = new List<Sepet>();

            List<SepetViewModels> result = (from a in _dbContext.Products.ToList()
                         join b in sepets
                         on a.ProductId equals b.ProductId
                         select new SepetViewModels
                         {
                             UrunAdi = a.ProductName,
                             Adet = b.Adet,
                             ProductId = a.ProductId,
                             Fiyat = a.UnitPrice,
                             ToplamFiyat = a.UnitPrice * b.Adet
                         }).ToList(); // sepetViewModels döner

            return View(result);
        }
    }
}
