using JobTracker.Domain.Entities;

namespace JobTracker.Domain.Interfaces;

public interface IContactRepository : IRepository<Contact>
{
    Task<Contact?> GetByJobApplicationAsync(Guid jobApplicationId);
}