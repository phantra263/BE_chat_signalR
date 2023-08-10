using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab.SignalR_Chat.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //private readonly IBookRepositoryAsync _bookRepositoryAsync;

        //public BooksController(IBookRepositoryAsync bookRepositoryAsync)
        //{
        //    _bookRepositoryAsync = bookRepositoryAsync;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get() => Ok(await _bookRepositoryAsync.GetAsync());

        //[HttpGet("{id:length(24)}", Name = "GetBook")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    var book = await _bookRepositoryAsync.GetAsync(id);

        //    if (book == null)
        //        return NotFound();

        //    return Ok(book);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Book book)
        //{
        //    await _bookRepositoryAsync.CreateAsync(book);

        //    return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        //}

        //[HttpPut("{id:length(24)}")]
        //public async Task<IActionResult> Update(string id, Book bookIn)
        //{
        //    var book = await _bookRepositoryAsync.GetAsync(id);

        //    if (book == null)
        //        return NotFound();

        //    await _bookRepositoryAsync.UpdateAsync(id, bookIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var book = await _bookRepositoryAsync.GetAsync(id);

        //    if (book == null)
        //        return NotFound();

        //    await _bookRepositoryAsync.RemoveAsync(id);

        //    return NoContent();
        //}
    }
}
