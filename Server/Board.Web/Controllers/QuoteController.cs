using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using Board.DataLayer;
using System;
using System.Linq;
using Board.Web.Models;

namespace Board.Web.Controllers
{
    [Route("api/[controller]")]
    public class QuoteController : Controller
    {
        private IQuoteRepository _repository;
        private ICategoryRepository _categoryRepository;

        public QuoteController(IQuoteRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }        

        [HttpGet]
        public IEnumerable<QuoteVm> GetAll(string author = "", string catid = "")
        {
            return _repository.GetQuotes()
                .Select(x => new QuoteVm { Id = x.Id, Author = x.Author, CategoryId = x.Category.Id.ToString(), CategoryTitle = x.Category.Title, Created = x.Created, Text = x.Text }).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var item = _repository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(new QuoteVm { Id = item.Id, Author = item.Author, CategoryId = item.Category.Id.ToString(), CategoryTitle = item.Category.Title, Created = item.Created, Text = item.Text });
        }

        [HttpPost()]
        public IActionResult Create([FromBody]QuoteAddVm model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var item = new Quote { Text = model.Text, Category = new Category { Id = Guid.Parse(model.CategoryId) }, Author = model.Author };
            _repository.Create(item);

            return CreatedAtRoute(new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] QuoteEditItem item)
        {
            if (item == null || Guid.Parse(item.Id) != id)
            {
                return BadRequest();
            }

            var todo = _repository.GetById(Guid.Parse(item.Id));
            if (todo == null)
            {
                return NotFound();
            }

            todo.Author = item.Author;
            todo.Category = new Category { Id = Guid.Parse(item.CategoryId) };
            todo.Text = item.Text;

            _repository.Update(todo);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_repository.Delete(id))
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
