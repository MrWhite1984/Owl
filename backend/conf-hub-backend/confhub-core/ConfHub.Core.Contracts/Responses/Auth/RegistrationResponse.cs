namespace ConfHub.Core.Contracts.Responses.Auth
{
    public record RegistrationResponse(Guid PersonId, string Token, string Role);
}
