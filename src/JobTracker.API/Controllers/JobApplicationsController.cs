using JobTracker.API.DTOs;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;
using JobTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationRepository _repository;
    private readonly ILogger<JobApplicationsController> _logger;

    public JobApplicationsController(
        IJobApplicationRepository repository,
        ILogger<JobApplicationsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplicationResponseDto>>> GetAll()
    {
        var applications = await _repository.GetAllAsync();
        var response = applications.Select(a => new JobApplicationResponseDto(
            a.Id, a.CompanyName, a.JobTitle, a.JobUrl,
            a.SalaryRange, a.Notes, a.Status, a.AppliedDate, a.CreatedAt));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobApplicationResponseDto>> GetById(Guid id)
    {
        var application = await _repository.GetWithDetailsAsync(id);
        if (application == null) return NotFound();
        return Ok(new JobApplicationResponseDto(
            application.Id, application.CompanyName, application.JobTitle,
            application.JobUrl, application.SalaryRange, application.Notes,
            application.Status, application.AppliedDate, application.CreatedAt));
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<JobApplicationResponseDto>>> GetByStatus(ApplicationStatus status)
    {
        var applications = await _repository.GetByStatusAsync(status);
        var response = applications.Select(a => new JobApplicationResponseDto(
            a.Id, a.CompanyName, a.JobTitle, a.JobUrl,
            a.SalaryRange, a.Notes, a.Status, a.AppliedDate, a.CreatedAt));
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<JobApplicationResponseDto>> Create(CreateJobApplicationDto dto)
    {
        var application = new JobApplication
        {
            CompanyName = dto.CompanyName,
            JobTitle = dto.JobTitle,
            JobUrl = dto.JobUrl,
            SalaryRange = dto.SalaryRange,
            Notes = dto.Notes,
            AppliedDate = dto.AppliedDate
        };
        var created = await _repository.AddAsync(application);
        _logger.LogInformation("Created job application for {Company}", dto.CompanyName);
        var response = new JobApplicationResponseDto(
            created.Id, created.CompanyName, created.JobTitle, created.JobUrl,
            created.SalaryRange, created.Notes, created.Status,
            created.AppliedDate, created.CreatedAt);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateJobApplicationDto dto)
    {
        var application = await _repository.GetByIdAsync(id);
        if (application == null) return NotFound();
        application.CompanyName = dto.CompanyName;
        application.JobTitle = dto.JobTitle;
        application.JobUrl = dto.JobUrl;
        application.SalaryRange = dto.SalaryRange;
        application.Notes = dto.Notes;
        application.Status = dto.Status;
        await _repository.UpdateAsync(application);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var application = await _repository.GetByIdAsync(id);
        if (application == null) return NotFound();
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}