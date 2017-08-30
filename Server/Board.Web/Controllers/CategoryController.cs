using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using Board.DataLayer;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }        

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetCategories();
        }        
    }
}
