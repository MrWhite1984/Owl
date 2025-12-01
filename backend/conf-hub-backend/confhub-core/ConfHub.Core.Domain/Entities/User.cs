namespace ConfHub.Core.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Role { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public User() { }
        public User(Guid id, string role, string passwordHash)
        {
            Id = id;
            Role = role;
            PasswordHash = passwordHash;
        }
    }
}
