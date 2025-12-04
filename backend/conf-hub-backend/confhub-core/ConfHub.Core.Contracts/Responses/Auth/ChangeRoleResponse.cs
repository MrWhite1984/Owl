namespace ConfHub.Core.Contracts.Responses.Auth
{
    public record ChangeRoleResponse(Guid PersonId, string Token, string Role);
}
