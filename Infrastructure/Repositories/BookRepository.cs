using LivrariaFuturo.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace LivrariaFuturo.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> Delete(int book_id);
        public Task<List<BookModel>> GetAll();
        public Task<List<BookModel>> Insert(string name, string author, int pageTotal, bool isActive, string? category);
        public Task<List<BookModel>> Update(int book_id, string name, string author, int pageTotal, bool isActive, string? category);
    }

    public class BookRepository : IBookRepository
    {
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "books";

        public BookRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<List<BookModel>> Delete(int book_id)
        {
            List<BookModel> books;
            _cache.TryGetValue<List<BookModel>>(cacheKey, out books);

            BookModel book = books?.Where(b => b.id == book_id).FirstOrDefault()!;

            books.Remove(book);

            _cache.Set(cacheKey, books);

            return Task.FromResult(books);
        }

        public Task<List<BookModel>> GetAll()
        {
            List<BookModel> books;
            _cache.TryGetValue<List<BookModel>>(cacheKey, out books);

            if (books == null) books = new List<BookModel>();

            return Task.FromResult(books);
        }

        public Task<List<BookModel>> Insert(string name, string author, int pageTotal, bool isActive, string? category)
        {
            List<BookModel> books;
            bool bookAlreadyExists = false;
            _cache.TryGetValue<List<BookModel>>(cacheKey, out books);

            if (books == null) books = new List<BookModel>();
             
            bookAlreadyExists = books?.Where(b => b.name == name && b.author == author && b.category == category).FirstOrDefault() != null;

            if (!bookAlreadyExists)
            {
                var id = books.Count() > 0 ? books.Last().id + 1 : 1;
                BookModel book = new BookModel(id, name, author, pageTotal, isActive, category);
                books.Add(book);
            }

            _cache.Set(cacheKey, books);

            return Task.FromResult(books);
        }

        public Task<List<BookModel>> Update(int book_id, string name, string author, int pageTotal, bool isActive, string? category)
        {
            List<BookModel> books;
            _cache.TryGetValue<List<BookModel>>(cacheKey, out books);

            BookModel book = books?.Where(b => b.id == book_id).FirstOrDefault()!;

            book.name = name;
            book.author = author;
            book.pageTotal = pageTotal;
            book.isActive = isActive;
            book.category = category;

            _cache.Set(cacheKey, books);

            return Task.FromResult(books);
        }
    }
}
