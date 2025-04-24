using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;
using Confluent.Kafka;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IProducer<Null, string> _producer;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;

        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
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

        await _producer.ProduceAsync("report-requests", new Message<Null, string> { Value = report.Id.ToString() });

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
        return r == null ? null : new ReportDto
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