using AspNetCoreExample.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AspNetCoreExample.Controllers
{
    public class ProductController : Controller
    {
        readonly AppDbContext dbContext;
        public ProductController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IActionResult Index(int? CatId)
        {
            var result = dbContext.Products.Where(c => c.CategoryId == CatId).OrderBy(c => c.ProductId).Take(6).ToList();

            int Count = dbContext.Products.Count(c => c.CategoryId == CatId); // Kategoriye göre ürün sayısı

            //ViewBag ve ViewData
            //Controller'dan view'a veya view'dan view'a veri transferi (küçük) için kullanılır
            //her ikiside key value şeklinde veri saklar.ViewBag asp.net mvc 3.0 ile gelmiştir.  ViewBag dynamic tiptir. tipi çalışmana zamanında oluşur..
            ViewData["productCount"] = Count;
            ViewBag.ProductCount = Count;
            ViewBag.CategoryId = CatId;


            return View(result);
        }

        public IActionResult Pagging(int? CatId, int PageId)
        {
            var result = dbContext.Products
                .Where(c => c.CategoryId == CatId)
                .OrderBy(c => c.ProductId)
                .Skip(PageId * 6)
                .Take(6)
                .ToList();

            return PartialView("_ProductList", result); // ajax request sonucu geriye partial view dön...
        }

        public IActionResult Detay(int ProductId)
        {
            var result = dbContext.Products.Find(ProductId);
            return View(result);
        }


        public IActionResult Search(string searchText)
        {
            var result = dbContext.Products.Where(c => c.ProductName.Contains(searchText)).ToList();

            return PartialView("_SearchResult", result);
        }
        public IActionResult SearchV2(string searchText)
        {
            var result = dbContext.Products.Where(c => c.ProductName.Contains(searchText)).ToList();

            //JsonSerializerOptions jOption = new JsonSerializerOptions();
            //jOption.PropertyNamingPolicy = null; // Default CamelCase (kelimeninilkharfiKüçükİkincisi Büyük)= null diyerek PascalCase yaptık..

            //return Json(result, jOption); // json'a parse eder sonuç döner...
            return Json(result);
        }
    }


    public class ProductVM
    {
        public List<Products> pList { get; set; }
        public int Count { get; set; }
    }
}