using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Entities.Enums;
using System;

namespace PhoneBook.Application.DTOs;

public class ContactCreateDto
{
    public Guid PersonId { get; set; }
    public ContactType Type { get; set; }
    public string Content { get; set; } = default!;
}