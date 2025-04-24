using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;
using PhoneBook.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly PhoneBookDbContext _context;

    public ReportRepository(PhoneBookDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Report report)
    {
        await _context.Reports.AddAsync(report);
    }

    public async Task<List<Report>> GetAllAsync()
    {
        return await _context.Reports.ToListAsync();
    }

    public async Task<Report?> GetByIdAsync(Guid id)
    {
        return await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}