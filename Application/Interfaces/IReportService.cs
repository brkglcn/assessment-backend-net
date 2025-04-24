using PhoneBook.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PhoneBook.Application.Interfaces;

public interface IReportService
{
    Task<Guid> RequestReportAsync(ReportCreateDto dto);
    Task<List<ReportDto>> GetAllReportsAsync();
    Task<ReportDto?> GetReportByIdAsync(Guid id);
}
