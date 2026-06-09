using JobTracker.Domain.Entities;

namespace JobTracker.Domain.Interfaces;

public interface IInterviewRepository : IRepository<Interview>
{
    Task<IEnumerable<Interview>> GetByJobApplicationAsync(Guid jobApplicationId);
    Task<IEnumerable<Interview>> GetUpcomingInterviewsAsync();
}