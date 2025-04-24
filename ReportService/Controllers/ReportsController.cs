using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace PhoneBook.ReportService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _reportService.GetAllReportsAsync();
        return Ok(reports);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var report = await _reportService.GetReportByIdAsync(id);
        return report == null ? NotFound() : Ok(report);
    }

    [HttpPost]
    public async Task<IActionResult> RequestReport([FromBody] ReportCreateDto dto)
    {
        var id = await _reportService.RequestReportAsync(dto);
        return Accepted(new { ReportId = id });
    }
}