namespace ConfHub.Core.Domain.Entities
{
    public class User
    {
        public Guid PersonId { get; private set; }
        public string Role { get; private set; } = default!;
        public User() { }
        public User(Guid personId, string role)
        {
            PersonId = personId;
            Role = role;
        }
    }
}
