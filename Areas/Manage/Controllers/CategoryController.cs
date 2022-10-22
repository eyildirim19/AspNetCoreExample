using AspNetCoreExample.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        CategoryRepository _repository;
        public CategoryController(
            CategoryRepository repository
            )
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var result = _repository.GetList();
            return View(result);
        }
    }
}