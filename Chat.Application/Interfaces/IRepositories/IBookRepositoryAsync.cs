using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IBookRepositoryAsync
    {
        Task<List<Book>> GetAsync();
        Task<Book> GetAsync(string id);
        Task<Book> CreateAsync(Book book);
        Task UpdateAsync(string id, Book bookIn);
        Task RemoveAsync(Book bookIn);
        Task RemoveAsync(string id);
    }
}
