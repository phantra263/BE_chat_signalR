﻿using Chat.Application.Features.Room.Queries.FindAnyAndGetLatestMessage;
using Chat.Application.Features.Room.Queries.FindOneAndGetLatestMessage;
using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IRoomRepositoryAsync
    {
        Task<Room> InsertOneAsync(Room room);
        Task<Room> FindOneByIdAsync(string roomId);
        Task<Room> FindOneByNameAsync(string name);
        Task<FindOneAndGetLatestMessageViewModel> FindOneAndGetLatestMessage(string roomId);
        Task<IList<FindAnyAndGetLatestMessageViewModel>> FindAnyAndGetLatestMessage(int pageNumber, int pageSize, string keyword);
    }
}
