using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Interfaces;

public interface IJobApplicationRepository : IRepository<JobApplication>
{
    Task<IEnumerable<JobApplication>> GetByStatusAsync(ApplicationStatus status);
    Task<IEnumerable<JobApplication>> GetByCompanyAsync(string companyName);
    Task<JobApplication?> GetWithDetailsAsync(Guid id);
}