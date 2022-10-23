using AspNetCoreExample.Models.Entities;
using AspNetCoreExample.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        //CategoryRepository _repository1;
        private readonly Repository<Categories> _repository;
        public CategoriesViewComponent(CategoryRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var result = _repository.GetList();
            return View(result);
        }
    }
}
