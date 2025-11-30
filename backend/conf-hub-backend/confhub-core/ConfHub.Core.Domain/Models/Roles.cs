using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Domain.Models
{
    public static class Roles
    {
        public const string Admin = "ADMIN";
        public const string Moderator = "MODERATOR";
        public const string SectionAdmin = "SECTION_ADMIN";
        public const string Reviewer = "REVIEWER";
        public const string User = "User";

        public static readonly string[] All =
        {
            Admin,
            Moderator,
            SectionAdmin,
            Reviewer,
            User
        };
    }
}
