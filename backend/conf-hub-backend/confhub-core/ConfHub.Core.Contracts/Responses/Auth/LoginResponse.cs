namespace ConfHub.Core.Contracts.Responses.Auth
{
    public record LoginResponse(
        string? Token = null,
        Guid? PersonId = null,
        string? Role = null,
        IEnumerable<string>? Roles = null);
}
