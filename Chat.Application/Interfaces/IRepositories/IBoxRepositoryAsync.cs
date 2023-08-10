using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IBoxRepositoryAsync
    {
        Task<IList<Box>> GetAsync();
        Task<Box> GetByIdAsync(string id);
        Task<Box> CreateAsync(Box box);
        Task UpdateAsync(string id, Box box);
        Task DeleteByIdAsync(string id);
        Task<Box> GetCheckExist(string user1Id, string user2Id);
        Task FindAndDeleteByUserAsync(string user1Id, string user2Id);
    }
}
