using AspNetCoreExample.Areas.Manage.Models;
using AspNetCoreExample.Models.Entities;
using AspNetCoreExample.Models.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryController(
            CategoryRepository repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            var result = _repository.GetList();
            //List<CategoryVM> vmResult = _repository.GetList().Select(c => new CategoryVM
            //{
            //    CategoryId = c.CategoryId,
            //    CategoryName = c.CategoryName,
            //    Description = c.Description
            //}).ToList();
            List<CategoryVM> vmResult = _mapper.Map<List<CategoryVM>>(result);
            return View(vmResult);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                //Categories categories = new Categories();
                //categories.CategoryName = model.CategoryName;
                //categories.Description = model.Description;
                Categories categories = _mapper.Map<Categories>(model);
                _repository.Add(categories);
            }
            return View();
        }
    }
}