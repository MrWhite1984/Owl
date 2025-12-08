namespace ConfHub.Core.Application.News.DTO
{
    public record AuthorDto(
        Guid Id,
        string Surname,
        string Name,
        string Patronymic,
        string EducationalInstitution,
        string JobTitle,
        string City,
        bool IsVerified);
}
