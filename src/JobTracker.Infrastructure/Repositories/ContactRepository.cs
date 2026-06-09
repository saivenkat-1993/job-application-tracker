using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Repositories;

public class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(JobTrackerDbContext context) : base(context)
    {
    }

    public async Task<Contact?> GetByJobApplicationAsync(Guid jobApplicationId)
        => await _dbSet
            .FirstOrDefaultAsync(c => c.JobApplicationId == jobApplicationId);
}