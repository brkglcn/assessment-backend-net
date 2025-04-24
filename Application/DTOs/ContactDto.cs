using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Entities.Enums;
using System;

namespace PhoneBook.Application.DTOs;

public class ContactDto
{
    public Guid Id { get; set; }
    public ContactType Type { get; set; }
    public string Content { get; set; } = default!;
}