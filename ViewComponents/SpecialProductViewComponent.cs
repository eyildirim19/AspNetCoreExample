using AspNetCoreExample.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.ViewComponents
{
    public class SpecialProductViewComponent : ViewComponent
    {
        readonly AppDbContext dbContext;
        public SpecialProductViewComponent(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IViewComponentResult Invoke()
        {
            int Count = dbContext.Products.Count();
            Random rnd = new Random();
            int RndVal = rnd.Next(0, Count); // 0 ile ürün sayısı arasında rasgele bir sayı oluştur

            // ElementAt =>index'e göre nesne döner...
            var product = dbContext.Products.ToList().ElementAt(RndVal); // oluşan rasgele indexi ürün olarak dön

            return View(product); //ürünü view'a bind et...
        }
    }
}
