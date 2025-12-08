namespace ConfHub.Core.Contracts.Responses.News
{
    public record AuthorInfo(
        string Surname,
        string Name,
        string Patronymic,
        string EducationalInstitution,
        string JobTitle,
        string City,
        bool IsVerified
    );
}
