using LivrariaFuturo.Authentication.Core.Domain;
using LivrariaFuturo.Authorization.Core.Domain;
using LivrariaFuturo.API.Models.Requests;
using LivrariaFuturo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LivrariaFuturo.API.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await this._bookService.GetAll();

            return new ApiResult(new Saida((int)HttpStatusCode.OK, true, "", books));
        }

        [Authorize()]
        [HttpPost("")]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest book)
        {
            var books = await this._bookService.Create(book.name, book.author, book.pageTotal, book.isActive, book.category);

            return new ApiResult(new Saida((int)HttpStatusCode.OK, true, "", books));
        }

        [Authorize()]
        [HttpDelete("{book_id}")]
        public async Task<IActionResult> DeleteBook(int book_id)
        {
            var books = await this._bookService.Delete(book_id);

            return new ApiResult(new Saida((int)HttpStatusCode.OK, true, "", books));
        }

        [Authorize()]
        [HttpPut("{book_id}")]
        public async Task<IActionResult> EditBook([FromBody] AddBookRequest book, int book_id)
        {
            var books = await this._bookService.Edit(book_id, book.name, book.author, book.pageTotal, book.isActive, book.category);

            return new ApiResult(new Saida((int)HttpStatusCode.OK, true, "", books));
        }


    }
}
