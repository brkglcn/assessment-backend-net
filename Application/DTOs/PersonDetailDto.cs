using System.Collections.Generic;

namespace PhoneBook.Application.DTOs;

public class PersonDetailDto : PersonDto
{
    public List<ContactDto> Contacts { get; set; } = new();
}