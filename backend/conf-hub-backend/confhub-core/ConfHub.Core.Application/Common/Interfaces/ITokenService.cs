namespace ConfHub.Core.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Guid personId, string role);
    }
}
