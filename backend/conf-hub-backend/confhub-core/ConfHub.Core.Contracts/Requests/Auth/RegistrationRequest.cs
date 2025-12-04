using System.ComponentModel.DataAnnotations;

namespace ConfHub.Core.Contracts.Requests.Auth
{
    public record RegistrationRequest(
        string Surname,
        string Name,
        string Patronymic,
        string EducationalInstitution,
        string JobTitle,
        string City,
        string Phone,
        [Required]
        [EmailAddress(ErrorMessage = "Некорректный формат email.")]
        string Email,
        bool IsVerified,
        string ElibraryProfileUrl,
        string Password);
}
