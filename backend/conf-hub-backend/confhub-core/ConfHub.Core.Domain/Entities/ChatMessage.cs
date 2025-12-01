using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid PersonId { get; private set; }
        public string Content { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public ChatMessage() { }
        public ChatMessage(Guid id, Guid projectId, Guid personId, string content, DateTime createdAt)
        {
            Id = id;
            ProjectId = projectId;
            PersonId = personId;
            Content = content;
            CreatedAt = createdAt;
        }
    }
}
