using ConfHub.Core.Application.ChatMessages.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly AppDbContext _context;

        public ChatMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ChatMessage chatMessage)
        {
            await _context.ChatMessages.AddAsync(chatMessage);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.ChatMessages.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();
        }

        public async Task<ChatMessage?> GetByIdAsync(Guid id)
        {
            var currentChatMessage = await _context.ChatMessages.FindAsync(id);
            return currentChatMessage;
        }

        public async Task<IEnumerable<ChatMessage>> GetByProjectIdAsync(Guid projectId)
        {
            var currentChatMessages = await _context.ChatMessages.Where(x => x.ProjectId.Equals(projectId)).ToListAsync();
            return currentChatMessages;
        }

        public void Update(ChatMessage chatMessage)
        {
            _context.ChatMessages.Update(chatMessage);
        }
    }
}
