namespace JobTracker.API.DTOs;

public record CreateContactDto(
    Guid JobApplicationId,
    string Name,
    string Email,
    string? Phone,
    string? Role
);

public record UpdateContactDto(
    string Name,
    string Email,
    string? Phone,
    string? Role
);

public record ContactResponseDto(
    Guid Id,
    Guid JobApplicationId,
    string Name,
    string Email,
    string? Phone,
    string? Role
);