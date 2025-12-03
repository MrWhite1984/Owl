namespace ConfHub.Core.Contracts.Requests.Users
{
    public record AddUserRequest(
        Guid PersonId, string Role
        );
}
