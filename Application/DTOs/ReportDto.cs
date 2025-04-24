using PhoneBook.Domain.Entities;
using System;

namespace PhoneBook.Application.DTOs;

public class ReportDto
{
    public Guid Id { get; set; }
    public string Location { get; set; } = default!;
    public DateTime RequestedAt { get; set; }
    public ReportStatus Status { get; set; }
    public int PersonCount { get; set; }
    public int PhoneNumberCount { get; set; }
}