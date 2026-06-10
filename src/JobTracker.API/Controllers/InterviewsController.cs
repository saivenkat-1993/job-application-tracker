using JobTracker.API.DTOs;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewsController : ControllerBase
{
    private readonly IInterviewRepository _repository;
    private readonly ILogger<InterviewsController> _logger;

    public InterviewsController(
        IInterviewRepository repository,
        ILogger<InterviewsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterviewResponseDto>>> GetAll()
    {
        var interviews = await _repository.GetAllAsync();
        var response = interviews.Select(i => new InterviewResponseDto(
            i.Id, i.JobApplicationId, i.InterviewDate,
            i.InterviewType, i.Notes, i.Outcome));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InterviewResponseDto>> GetById(Guid id)
    {
        var interview = await _repository.GetByIdAsync(id);
        if (interview == null) return NotFound();
        return Ok(new InterviewResponseDto(
            interview.Id, interview.JobApplicationId, interview.InterviewDate,
            interview.InterviewType, interview.Notes, interview.Outcome));
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<IEnumerable<InterviewResponseDto>>> GetUpcoming()
    {
        var interviews = await _repository.GetUpcomingInterviewsAsync();
        var response = interviews.Select(i => new InterviewResponseDto(
            i.Id, i.JobApplicationId, i.InterviewDate,
            i.InterviewType, i.Notes, i.Outcome));
        return Ok(response);
    }

    [HttpGet("application/{jobApplicationId}")]
    public async Task<ActionResult<IEnumerable<InterviewResponseDto>>> GetByApplication(Guid jobApplicationId)
    {
        var interviews = await _repository.GetByJobApplicationAsync(jobApplicationId);
        var response = interviews.Select(i => new InterviewResponseDto(
            i.Id, i.JobApplicationId, i.InterviewDate,
            i.InterviewType, i.Notes, i.Outcome));
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<InterviewResponseDto>> Create(CreateInterviewDto dto)
    {
        var interview = new Interview
        {
            JobApplicationId = dto.JobApplicationId,
            InterviewDate = dto.InterviewDate,
            InterviewType = dto.InterviewType,
            Notes = dto.Notes
        };
        var created = await _repository.AddAsync(interview);
        _logger.LogInformation("Created interview for application {Id}", dto.JobApplicationId);
        var response = new InterviewResponseDto(
            created.Id, created.JobApplicationId, created.InterviewDate,
            created.InterviewType, created.Notes, created.Outcome);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateInterviewDto dto)
    {
        var interview = await _repository.GetByIdAsync(id);
        if (interview == null) return NotFound();
        interview.InterviewDate = dto.InterviewDate;
        interview.InterviewType = dto.InterviewType;
        interview.Notes = dto.Notes;
        interview.Outcome = dto.Outcome;
        await _repository.UpdateAsync(interview);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var interview = await _repository.GetByIdAsync(id);
        if (interview == null) return NotFound();
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}