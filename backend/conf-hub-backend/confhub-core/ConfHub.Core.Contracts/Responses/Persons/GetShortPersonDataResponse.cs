namespace ConfHub.Core.Contracts.Responses.Persons
{
    public record GetShortPersonDataResponse(
        Guid Id,
        string Surname,
        string Name,
        string Patronymic,
        string EducationalInstitution,
        string JobTitle,
        string City);
}
