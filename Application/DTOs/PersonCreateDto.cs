namespace PhoneBook.Application.DTOs;

public class PersonCreateDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Company { get; set; } = default!;
}