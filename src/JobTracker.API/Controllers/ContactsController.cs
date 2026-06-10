using JobTracker.API.DTOs;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _repository;
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(
        IContactRepository repository,
        ILogger<ContactsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactResponseDto>>> GetAll()
    {
        var contacts = await _repository.GetAllAsync();
        var response = contacts.Select(c => new ContactResponseDto(
            c.Id, c.JobApplicationId, c.Name, c.Email, c.Phone, c.Role));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactResponseDto>> GetById(Guid id)
    {
        var contact = await _repository.GetByIdAsync(id);
        if (contact == null) return NotFound();
        return Ok(new ContactResponseDto(
            contact.Id, contact.JobApplicationId,
            contact.Name, contact.Email, contact.Phone, contact.Role));
    }

    [HttpGet("application/{jobApplicationId}")]
    public async Task<ActionResult<ContactResponseDto>> GetByApplication(Guid jobApplicationId)
    {
        var contact = await _repository.GetByJobApplicationAsync(jobApplicationId);
        if (contact == null) return NotFound();
        return Ok(new ContactResponseDto(
            contact.Id, contact.JobApplicationId,
            contact.Name, contact.Email, contact.Phone, contact.Role));
    }

    [HttpPost]
    public async Task<ActionResult<ContactResponseDto>> Create(CreateContactDto dto)
    {
        var contact = new Contact
        {
            JobApplicationId = dto.JobApplicationId,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Role = dto.Role
        };
        var created = await _repository.AddAsync(contact);
        _logger.LogInformation("Created contact {Name} for application {Id}", dto.Name, dto.JobApplicationId);
        var response = new ContactResponseDto(
            created.Id, created.JobApplicationId,
            created.Name, created.Email, created.Phone, created.Role);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateContactDto dto)
    {
        var contact = await _repository.GetByIdAsync(id);
        if (contact == null) return NotFound();
        contact.Name = dto.Name;
        contact.Email = dto.Email;
        contact.Phone = dto.Phone;
        contact.Role = dto.Role;
        await _repository.UpdateAsync(contact);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var contact = await _repository.GetByIdAsync(id);
        if (contact == null) return NotFound();
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}