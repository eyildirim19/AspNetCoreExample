using AspNetCoreExample.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        AppDbContext dbContext; // = new AppDbContext();
		public CategoriesViewComponent(AppDbContext _dbContext)
		{
			dbContext = _dbContext;
		}
		public IViewComponentResult Invoke()
        {
            //AppDbContext dbContext= new AppDbContext();
            var result = dbContext.Categories.ToList();

            return View(result);
        }
    }
}
