using ConfHub.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Application.ChatMessages.Interfaces
{
    public interface IChatMessageService
    {
        Task<ChatMessage?> GetByIdAsync(Guid id);
        Task<IEnumerable<ChatMessage>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(Guid projectId, Guid personId, string content, DateTime createdAt);
        void Update(ChatMessage chatMessage);
        Task DeleteAsync(Guid id);
    }
}
