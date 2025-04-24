using Confluent.Kafka;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace PhoneBook.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IProducer<Null, string> _producer;

    public ReportService(IReportRepository reportRepository, IProducer<Null, string> producer)
    {
        _reportRepository = reportRepository;
        _producer = producer;
    }

    public async Task<Guid> RequestReportAsync(ReportCreateDto dto)
    {
        var report = new Report
        {
            Id = Guid.NewGuid(),
            RequestedAt = DateTime.UtcNow,
            Status = ReportStatus.Preparing,
            Location = dto.Location,
            PersonCount = 0,
            PhoneNumberCount = 0
        };

        await _reportRepository.AddAsync(report);
        await _reportRepository.SaveChangesAsync();

        var kafkaMessage = new Message<Null, string> { Value = report.Id.ToString() };
        await _producer.ProduceAsync("report-requests", kafkaMessage);

        return report.Id;
    }

    public async Task<List<ReportDto>> GetAllReportsAsync()
    {
        var reports = await _reportRepository.GetAllAsync();

        return reports.Select(r => new ReportDto
        {
            Id = r.Id,
            Location = r.Location,
            RequestedAt = r.RequestedAt,
            Status = r.Status,
            PersonCount = r.PersonCount,
            PhoneNumberCount = r.PhoneNumberCount
        }).ToList();
    }

    public async Task<ReportDto?> GetReportByIdAsync(Guid id)
    {
        var r = await _reportRepository.GetByIdAsync(id);
        if (r == null) return null;

        return new ReportDto
        {
            Id = r.Id,
            Location = r.Location,
            RequestedAt = r.RequestedAt,
            Status = r.Status,
            PersonCount = r.PersonCount,
            PhoneNumberCount = r.PhoneNumberCount
        };
    }
}
