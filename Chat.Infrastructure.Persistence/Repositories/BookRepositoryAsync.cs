using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class BookRepositoryAsync : IBookRepositoryAsync
    {
        private readonly IMongoCollection<Book> _books;

        public BookRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase("BookstoreDb");

            _books = database.GetCollection<Book>(Collections.BookCollection);
        }

        public async Task<List<Book>> GetAsync() =>
            await _books.Find(book => true).ToListAsync();

        public async Task<Book> GetAsync(string id) =>
            await _books.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Book> CreateAsync(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task UpdateAsync(string id, Book bookIn) =>
            await _books.ReplaceOneAsync(x => x.Id == id, bookIn);

        public async Task RemoveAsync(Book bookIn) =>
            await _books.DeleteOneAsync(x => x.Id == bookIn.Id);

        public async Task RemoveAsync(string id) =>
            await _books.DeleteOneAsync(x => x.Id == id);
    }
}
