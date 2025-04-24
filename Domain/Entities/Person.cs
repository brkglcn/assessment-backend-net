using System.Collections.Generic;
using System;

namespace PhoneBook.Domain.Entities;

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Company { get; set; } = default!;
    public ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
}
