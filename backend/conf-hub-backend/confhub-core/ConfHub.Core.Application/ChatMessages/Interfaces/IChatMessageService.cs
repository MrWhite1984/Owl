using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.ChatMessages.Interfaces
{
    public interface IChatMessageService
    {
        Task<ChatMessage?> GetByIdAsync(Guid id);
        Task<IEnumerable<ChatMessage>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(Guid projectId, Guid personId, string content);
        Task UpdateAsync(ChatMessage chatMessage);
        Task DeleteAsync(Guid id);
    }
}
