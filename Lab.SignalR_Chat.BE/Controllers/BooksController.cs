using Lab.SignalR_Chat.BE.Context.Entities;
using Lab.SignalR_Chat.BE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab.SignalR_Chat.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _bookService.GetAsync());

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public async Task<IActionResult> Get(string id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            await _bookService.CreateAsync(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book bookIn)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
                return NotFound();

            await _bookService.UpdateAsync(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
                return NotFound();

            await _bookService.RemoveAsync(id);

            return NoContent();
        }
    }
}
