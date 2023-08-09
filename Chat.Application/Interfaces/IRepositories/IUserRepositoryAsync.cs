using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IUserRepositoryAsync
    {
        Task<IList<User>> GetAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> GetByNickNameAsync(string nickname);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
    }
}
