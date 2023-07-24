using LivrariaFuturo.Infrastructure.Models;
using LivrariaFuturo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaFuturo.Application.Services
{
    public interface IBookService
    {
        public Task<List<BookModel>> Create(string name, string author, int pageTotal, bool isActive, string? category);
        public Task<List<BookModel>> Delete(int book_id);
        public Task<List<BookModel>> Edit(int book_id, string name, string author, int pageTotal, bool isActive, string? category);
        public Task<List<BookModel>> GetAll();
    }

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public Task<List<BookModel>> Create(string name, string author, int pageTotal, bool isActive, string? category)
        {
            return this._bookRepository.Insert(name, author, pageTotal, isActive, category);
        }

        public Task<List<BookModel>> Delete(int book_id)
        {
            return this._bookRepository.Delete(book_id);
        }

        public Task<List<BookModel>> Edit(int book_id, string name, string author, int pageTotal, bool isActive, string? category)
        {
            return this._bookRepository.Update(book_id, name, author, pageTotal, isActive, category);
        }

        public Task<List<BookModel>> GetAll()
        {
            return this._bookRepository.GetAll();
        }
    }
}
