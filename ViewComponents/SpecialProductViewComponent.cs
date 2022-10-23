using AspNetCoreExample.Models.Entities;
using AspNetCoreExample.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.ViewComponents
{
    public class SpecialProductViewComponent : ViewComponent
    {
        private readonly Repository<Products> _repository;
        public SpecialProductViewComponent(ProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            int Count = _repository.GetList().Count();
            Random rnd = new Random();
            int RndVal = rnd.Next(0, Count); // 0 ile ürün sayısı arasında rasgele bir sayı oluştur

            // ElementAt =>index'e göre nesne döner...
            var product = _repository.GetList().ElementAt(RndVal); // oluşan rasgele indexi ürün olarak dön

            return View(product); //ürünü view'a bind et...
        }
    }
}