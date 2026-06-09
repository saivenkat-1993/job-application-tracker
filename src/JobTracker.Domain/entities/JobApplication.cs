using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Entities;

public class JobApplication : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string? JobUrl { get; set; }
    public string? SalaryRange { get; set; }
    public string? Notes { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    public Contact? Contact { get; set; }
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}