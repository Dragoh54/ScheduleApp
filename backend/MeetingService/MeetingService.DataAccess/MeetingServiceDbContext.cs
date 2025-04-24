using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingService.DataAccess;

public class MeetingServiceDbContext(
    DbContextOptions<MeetingServiceDbContext> options
    ) : DbContext(options)
{
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Participant> Participants { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingServiceDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}