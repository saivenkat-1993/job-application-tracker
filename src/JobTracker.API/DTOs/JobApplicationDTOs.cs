using JobTracker.Domain.Enums;

namespace JobTracker.API.DTOs;

public record CreateJobApplicationDto(
    string CompanyName,
    string JobTitle,
    string? JobUrl,
    string? SalaryRange,
    string? Notes,
    DateTime AppliedDate
);

public record UpdateJobApplicationDto(
    string CompanyName,
    string JobTitle,
    string? JobUrl,
    string? SalaryRange,
    string? Notes,
    ApplicationStatus Status
);

public record JobApplicationResponseDto(
    Guid Id,
    string CompanyName,
    string JobTitle,
    string? JobUrl,
    string? SalaryRange,
    string? Notes,
    ApplicationStatus Status,
    DateTime AppliedDate,
    DateTime CreatedAt
);