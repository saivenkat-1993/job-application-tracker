using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Entities;

public class Interview : BaseEntity
{
    public Guid JobApplicationId { get; set; }
    public JobApplication JobApplication { get; set; } = null!;
    public DateTime InterviewDate { get; set; }
    public InterviewType InterviewType { get; set; }
    public string? Notes { get; set; }
    public string? Outcome { get; set; }
}