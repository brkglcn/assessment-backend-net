using PhoneBook.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportService.Jobs
{
    public class ReportJob : IReportJob
    {
        private readonly IReportService _reportService;

        public ReportJob(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task ExecuteAsync(Guid reportId)
        {
            await _reportService.GetReportByIdAsync(reportId);
        }
    }
}