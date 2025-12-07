using System.ComponentModel.DataAnnotations;

namespace ConfHub.Core.Contracts.Responses.Persons
{
    public record GetFullPersonDataResponse(
        Guid Id,
        string Surname,
        string Name,
        string Patronymic,
        string EducationalInstitution,
        string JobTitle,
        string City,
        string Phone,
        string Email,
        bool IsVerified,
        string ElibraryProfileUrl);
}
