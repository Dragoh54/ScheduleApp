using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.EntityFrameworkCore.Extensions;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.DatabaseContext;

public class ScheduleServiceDbContext(DbContextOptions<ScheduleServiceDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    public DbSet<CalendarDay> CalendarDays { get; set; } = null!;
    public DbSet<DailySchedule> DailySchedules { get; set; } = null!;
    //public DbSet<TimeSlot> TimeSlots { get; set; } = null!;
    
    private readonly IConfiguration _configuration = configuration;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalendarDay>().ToCollection("days");
        modelBuilder.Entity<DailySchedule>().ToCollection("daily_schedules");
    }
}