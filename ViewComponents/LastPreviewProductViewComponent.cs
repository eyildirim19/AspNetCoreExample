﻿using AspNetCoreExample.Models;
using AspNetCoreExample.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCoreExample.ViewComponents
{

    public class LastPreviewProduct : ViewComponent
    {
        AppDbContext dbContext;
        public LastPreviewProduct(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        // ProductId => incelenen ürün id'si
        public IViewComponentResult Invoke(int ProductId)
        {

            List<PreviewProduct> pList = new List<PreviewProduct>();
            List<Products> products = new List<Products>();

            string tempList;
            HttpContext.Request.Cookies.TryGetValue("prods", out tempList);
            // cooki'de value varsa...
            if (tempList != null)
                pList = JsonConvert.DeserializeObject<List<PreviewProduct>>(tempList);

            if (pList.Count > 0)
            {
                List<int> Ids = pList.Select(c => c.ProductId).ToList(); // prpductId seç
                // id'si Ids listesinde geçenleri getir..
                products = dbContext.Products.Where(c => Ids.Contains(c.ProductId) && c.ProductId != ProductId).ToList();
            }

            

            return View(products);

        }
    }
}
