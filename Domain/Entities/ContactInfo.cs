using PhoneBook.Domain.Entities.Enums;
using System;

namespace PhoneBook.Domain.Entities;

public class ContactInfo
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public ContactType Type { get; set; }
    public string Content { get; set; } = default!;

    public Person Person { get; set; } = default!;
}