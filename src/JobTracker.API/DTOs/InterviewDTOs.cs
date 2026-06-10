using JobTracker.Domain.Enums;

namespace JobTracker.API.DTOs;

public record CreateInterviewDto(
    Guid JobApplicationId,
    DateTime InterviewDate,
    InterviewType InterviewType,
    string? Notes
);

public record UpdateInterviewDto(
    DateTime InterviewDate,
    InterviewType InterviewType,
    string? Notes,
    string? Outcome
);

public record InterviewResponseDto(
    Guid Id,
    Guid JobApplicationId,
    DateTime InterviewDate,
    InterviewType InterviewType,
    string? Notes,
    string? Outcome
);