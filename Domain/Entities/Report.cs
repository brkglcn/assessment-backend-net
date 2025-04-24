using System;

namespace PhoneBook.Domain.Entities;

public class Report
{
    public Guid Id { get; set; }
    public DateTime RequestedAt { get; set; }
    public ReportStatus Status { get; set; }
    public string Location { get; set; } = default!;
    public int PersonCount { get; set; }
    public int PhoneNumberCount { get; set; }
}