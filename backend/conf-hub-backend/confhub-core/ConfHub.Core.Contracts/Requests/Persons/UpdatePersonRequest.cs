using System.ComponentModel.DataAnnotations;

namespace ConfHub.Core.Contracts.Requests.Persons
{
    public record UpdatePersonRequest(
    string? Surname = null,
    string? Name = null,
    string? Patronymic = null,
    string? EducationalInstitution = null,
    string? JobTitle = null,
    string? City = null,
    string? Phone = null,
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    string? Email = null,
    bool? IsVerified = null,
    bool? IsDeleted = null,
    string? ElibraryProfileUrl = null,
    string? NewPassword = null
);
}
