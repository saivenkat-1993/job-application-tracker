using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Repositories;

public class InterviewRepository : BaseRepository<Interview>, IInterviewRepository
{
    public InterviewRepository(JobTrackerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Interview>> GetByJobApplicationAsync(Guid jobApplicationId)
        => await _dbSet
            .Where(i => i.JobApplicationId == jobApplicationId)
            .OrderBy(i => i.InterviewDate)
            .ToListAsync();

    public async Task<IEnumerable<Interview>> GetUpcomingInterviewsAsync()
        => await _dbSet
            .Where(i => i.InterviewDate >= DateTime.UtcNow)
            .OrderBy(i => i.InterviewDate)
            .Include(i => i.JobApplication)
            .ToListAsync();
}