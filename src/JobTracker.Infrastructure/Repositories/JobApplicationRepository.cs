using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Repositories;

public class JobApplicationRepository : BaseRepository<JobApplication>, IJobApplicationRepository
{
    public JobApplicationRepository(JobTrackerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<JobApplication>> GetByStatusAsync(ApplicationStatus status)
        => await _dbSet.Where(j => j.Status == status).ToListAsync();

    public async Task<IEnumerable<JobApplication>> GetByCompanyAsync(string companyName)
        => await _dbSet
            .Where(j => j.CompanyName.ToLower().Contains(companyName.ToLower()))
            .ToListAsync();

    public async Task<JobApplication?> GetWithDetailsAsync(Guid id)
        => await _dbSet
            .Include(j => j.Interviews)
            .Include(j => j.Contact)
            .FirstOrDefaultAsync(j => j.Id == id);
}