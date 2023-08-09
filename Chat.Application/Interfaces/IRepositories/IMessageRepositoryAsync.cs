using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IMessageRepositoryAsync
    {
        Task<IList<Message>> GetAsync();
        Task<Message> GetByIdAsync(string id);
        Task<Message> CreateAsync(Message message);
        Task UpdateAsync(string id, Message message);
        Task DeleteAsync(string id);
    }
}
