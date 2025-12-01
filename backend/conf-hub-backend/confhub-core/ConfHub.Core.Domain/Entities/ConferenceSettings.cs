using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Domain.Entities
{
    public class ConferenceSettings
    {
        public Guid Id { get; private set; }
        public int MaxArticlesPerAuthor { get; private set; }
        public bool AllowOnlineDefence { get; private set; }
        public bool IsPublicPageEnabled { get; private set; }
        public ConferenceSettings() { }
        public ConferenceSettings(Guid id, int maxArticlesPerAuthor, bool allowOnlineDefence, bool isPublicPageEnabled)
        {
            Id = id;
            MaxArticlesPerAuthor = maxArticlesPerAuthor;
            AllowOnlineDefence = allowOnlineDefence;
            IsPublicPageEnabled = isPublicPageEnabled;
        }
    }
}
