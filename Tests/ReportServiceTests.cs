using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.Services;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;
using Confluent.Kafka;
using FluentAssertions;

public class ReportServiceTests
{
    private readonly Mock<IReportRepository> _mockRepo;
    private readonly Mock<IProducer<Null, string>> _mockProducer;
    private readonly ReportService _service;

    public ReportServiceTests()
    {
        _mockRepo = new Mock<IReportRepository>();
        _mockProducer = new Mock<IProducer<Null, string>>();
        _mockProducer.Setup(p => p.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<Null, string>>(), default))
                     .ReturnsAsync(new DeliveryResult<Null, string>());

        _service = new ReportService(_mockRepo.Object, _mockProducer.Object);
    }

    [Fact]
    public async Task RequestReportAsync_ShouldReturnGuid()
    {
        var dto = new ReportCreateDto { Location = "Istanbul" };

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Report>())).Returns(Task.CompletedTask);
        _mockRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _service.RequestReportAsync(dto);

        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetAllReportsAsync_ShouldReturnReportList()
    {
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Report>
        {
            new Report
            {
                Id = Guid.NewGuid(),
                Location = "Ankara",
                RequestedAt = DateTime.UtcNow,
                Status = ReportStatus.Completed,
                PersonCount = 2,
                PhoneNumberCount = 3
            }
        });

        var result = await _service.GetAllReportsAsync();

        result.Should().HaveCount(1);
        result[0].Location.Should().Be("Ankara");
    }

    [Fact]
    public async Task GetReportByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Report?)null);

        var result = await _service.GetReportByIdAsync(Guid.NewGuid());

        result.Should().BeNull();
    }
}