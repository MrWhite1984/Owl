using ConfHub.Core.Application.ChatMessages.Interfaces;
using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.ChatMessages.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChatMessageService(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Guid projectId, Guid personId, string content)
        {
            ChatMessage chatMessage = new ChatMessage(Guid.NewGuid(), projectId, personId, content, DateTime.UtcNow);
            await _chatMessageRepository.AddAsync(chatMessage);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _chatMessageRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ChatMessage?> GetByIdAsync(Guid id)
        {
            var currentChatMessage = await _chatMessageRepository.GetByIdAsync(id);
            return currentChatMessage;
        }

        public async Task<IEnumerable<ChatMessage>> GetByProjectIdAsync(Guid projectId)
        {
            var currentChatMessage = await _chatMessageRepository.GetByProjectIdAsync(projectId);
            return currentChatMessage;
        }

        public async Task UpdateAsync(ChatMessage chatMessage)
        {
            _chatMessageRepository.Update(chatMessage);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
