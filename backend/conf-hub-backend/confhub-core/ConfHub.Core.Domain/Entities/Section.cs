using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Domain.Entities
{
    public class Section
    {
        public Guid Id { get; private set; }
        public Guid ConferenceId { get; private set; }
        public string Title { get; private set; } = default!;
        public Section() { }
        public Section(Guid id, Guid conferenceId, string title)
        {
            Id = id;
            ConferenceId = conferenceId;
            Title = title;
        }
    }
}
