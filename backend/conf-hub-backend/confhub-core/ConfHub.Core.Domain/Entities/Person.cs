namespace ConfHub.Core.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; private set; }
        public string Surname { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Patronymic { get; private set; } = default!;
        public string EducationalInstitution { get; private set; } = default!;
        public string JobTitle { get; private set; } = default!;
        public string City { get; private set; } = default!;
        public string Phone {  get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public bool IsVerified { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public string ElibraryProfileUrl { get; private set; } = default!;
        public string PhotoUrl { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; } = default!;

        public Person() { }
        public Person(Guid id, string surname, string name, string patronymic, string educationalInstitution, string jobTitle, string city, string phone, string email, bool isVerified, bool isDeleted, string elibraryProfileUrl, string photoUrl, DateTime createdAt)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            EducationalInstitution = educationalInstitution;
            JobTitle = jobTitle;
            City = city;
            Phone = phone;
            Email = email;
            IsVerified = isVerified;
            IsDeleted = isDeleted;
            ElibraryProfileUrl = elibraryProfileUrl;
            PhotoUrl = photoUrl;
            CreatedAt = createdAt;
        }
    }
}
