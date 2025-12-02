using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Domain.Entities
{
    public class ConferenceSettings
    {
        public Guid ConferenceId { get; private set; }
        public int MaxArticlesPerAuthor { get; private set; }
        public bool AllowOnlineDefence { get; private set; }
        public bool IsPublicPageEnabled { get; private set; }
        public ConferenceSettings() { }
        public ConferenceSettings(Guid conferenceId, int maxArticlesPerAuthor, bool allowOnlineDefence, bool isPublicPageEnabled)
        {
            ConferenceId = conferenceId;
            MaxArticlesPerAuthor = maxArticlesPerAuthor;
            AllowOnlineDefence = allowOnlineDefence;
            IsPublicPageEnabled = isPublicPageEnabled;
        }
    }
}
