using AspNetCoreExample.Models;
using AspNetCoreExample.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public IActionResult Detay(int ProductId, string ProductName)
        {
            // bakınız var konusu...
            // ürünü bul
            // Find metodu geriye tek bir obje döner. hangi entites ürezinden çağrılırsa gönderilen parametreyi pk'da arar o tipte obje döner. 

            var result = dbContext.Products.Find(ProductId);

            // en son incelediğimiz ürünü cookie'ye yazalım...
            SetIncelenenUrun(result);

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


        private void SetIncelenenUrun(Products prod)
        {
            //   HttpContext.Response.Cookies.Append("prods", result); veri string olarak saklanacağı için product olarak cookiye set edemeyiz...
            // birden fazla ürün inceleneceği için ürünleri bir listeye atıyoruz...
            List<PreviewProduct> prodList = new List<PreviewProduct>();


            // önce listeyi string'e çevilerim. sonra cookiye ekleyelim...
            string temp = prodList.ToString(); // bu şekilde stringe çeviremeyiz. çünkü elimizdeki liste strine dönüştürüldüğünde veri olarak değil namespace.isim olarak dönüştürülür..


            string tempArray;
            HttpContext.Request.Cookies.TryGetValue("prods", out tempArray);
            if (tempArray != null)
                prodList = JsonConvert.DeserializeObject<List<PreviewProduct>>(tempArray); // string tipindeki json'ı objecte dönüştürür

            // 10 adet ürün ekleyelimm

            // listeye eklenen ürün birdaha eklenmesin...
            if (prodList.FirstOrDefault(c => c.ProductId == prod.ProductId) == null)
            {
                PreviewProduct p = new PreviewProduct();
                p.ProductId = prod.ProductId;
                p.PreviewDate = DateTime.Now;
                prodList.Add(p);


                prodList = prodList.OrderByDescending(c => c.PreviewDate).ToList();

                if (prodList.Count >= 4) // 4'dn fazla ürün varsa
                {
                    var lastItem = prodList.Last(); // listenin son elemanını al
                    prodList.Remove(lastItem); // listeden çıkar..
                }
            }

            // ürün listesinesi json array olarak stringe ata..
            string strArray = JsonConvert.SerializeObject(prodList); // object'i json stringe dönüştür

            CookieOptions options = new CookieOptions();
            options.Expires = DateTimeOffset.Now.AddDays(10);

            HttpContext.Response.Cookies.Append("prods", strArray, options);
        }
    }

    public class ProductVM
    {
        public List<Products> pList { get; set; }
        public int Count { get; set; }
    }
}