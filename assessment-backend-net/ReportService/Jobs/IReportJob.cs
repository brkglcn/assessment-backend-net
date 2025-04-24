using System;
using System.Threading.Tasks;

namespace ReportService.Jobs
{
    public interface IReportJob
    {
        Task ExecuteAsync(Guid reportId);
    }
}