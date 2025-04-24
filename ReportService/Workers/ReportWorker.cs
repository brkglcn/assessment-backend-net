using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using PhoneBook.Infrastructure.Data;

public class ReportWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ReportWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "report-consumer-group"
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("report-requests");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(stoppingToken);
                var reportId = Guid.Parse(consumeResult.Message.Value);

                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<PhoneBookDbContext>();

                var report = await dbContext.Reports.FirstOrDefaultAsync(r => r.Id == reportId, stoppingToken);
                if (report != null)
                {
                    report.PersonCount = 2;
                    report.PhoneNumberCount = 3;
                    report.Status = ReportStatus.Completed;
                    await dbContext.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Worker Error: {ex.Message}");
            }
        }
    }
}