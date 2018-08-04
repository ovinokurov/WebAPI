using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private readonly DataContext _context;

        public DataController(DataContext context)
        {
            _context = context;

            if (_context.DataItems.Count() == 0)
            {
                _context.DataItems.Add(new DataItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<DataItem>> GetAll()
        {
            return _context.DataItems.ToList();
        }

        [HttpGet("{id}", Name = "GetData")]
        public ActionResult<DataItem> GetById(long id)
        {
            var item = _context.DataItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(DataItem item)
        {
            _context.DataItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetData", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, DataItem item)
        {
            var todo = _context.DataItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.DataItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.DataItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.DataItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

    }
}