using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingService.DataAccess;

public class MeetingServiceDbContext : DbContext
{
    public MeetingServiceDbContext(DbContextOptions<MeetingServiceDbContext> options) : base(options)
    {
    }
    
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<ScheduledJob> ScheduledJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingServiceDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}