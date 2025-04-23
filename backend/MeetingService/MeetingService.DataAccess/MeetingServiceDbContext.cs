using Microsoft.EntityFrameworkCore;

namespace MeetingService.DataAccess;

public class MeetingServiceDbContext(
    DbContextOptions<MeetingServiceDbContext> options
    ) : DbContext(options)
{
    //TODO: ADD DB SETS
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingServiceDbContext).Assembly);
        
        //TODO: ADD INDEXES
        
        base.OnModelCreating(modelBuilder);
    }
}