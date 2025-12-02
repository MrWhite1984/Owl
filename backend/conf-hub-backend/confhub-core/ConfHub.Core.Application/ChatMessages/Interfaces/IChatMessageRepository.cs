using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.ChatMessages.Interfaces
{
    public interface IChatMessageRepository
    {
        Task<ChatMessage?> GetByIdAsync(Guid id);
        Task<IEnumerable<ChatMessage>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(ChatMessage chatMessage);
        void Update(ChatMessage chatMessage);
        Task DeleteAsync(Guid id);
    }
}
