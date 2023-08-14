using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IUserRepositoryAsync
    {
        Task<IReadOnlyList<User>> GetAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> GetByNicknameAsync(string nickname);
        Task<IReadOnlyList<User>> GetListByNicknameAsync(string nickname, string yourNickname);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
        Task<User> Authenticate(string nickname, string password);
    }
}
