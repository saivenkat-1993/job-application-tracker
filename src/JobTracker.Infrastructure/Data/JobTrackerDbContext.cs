using JobTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Data;

public class JobTrackerDbContext : DbContext
{
    public JobTrackerDbContext(DbContextOptions<JobTrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(200);
            entity.Property(e => e.JobUrl).HasMaxLength(500);
            entity.Property(e => e.SalaryRange).HasMaxLength(100);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasMany(e => e.Interviews)
                  .WithOne(e => e.JobApplication)
                  .HasForeignKey(e => e.JobApplicationId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Contact)
                  .WithOne(e => e.JobApplication)
                  .HasForeignKey<Contact>(e => e.JobApplicationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.InterviewType).HasConversion<string>();
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(100);
        });
    }
}