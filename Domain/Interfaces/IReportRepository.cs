using PhoneBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Interfaces;

public interface IReportRepository
{
    Task<Report?> GetByIdAsync(Guid id);
    Task<List<Report>> GetAllAsync();
    Task AddAsync(Report report);
    Task SaveChangesAsync();
}