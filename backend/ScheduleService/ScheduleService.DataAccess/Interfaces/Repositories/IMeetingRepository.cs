using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Interfaces.Repositories;

public interface IMeetingRepository : IBaseRepository<Meeting>
{
   /// <summary>
   /// Get meeting for user on certain date
   /// </summary>
   /// <param name="userId"></param>
   /// <param name="date"></param>
   /// <param name="cancellationToken"></param>
   /// <returns></returns>
   public Task<IEnumerable<Meeting>> GetMeetingsForUserOnDateAsync(Guid userId, DateOnly date, CancellationToken cancellationToken);

   /// <summary>
   /// Get meetings for user in range
   /// </summary>
   /// <param name="userId"></param>
   /// <param name="from"></param>
   /// <param name="to"></param>
   /// <param name="cancellationToken"></param>
   /// <returns></returns>
   public Task<IEnumerable<Meeting>> GetMeetingsForUserInRangeAsync(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken);
   
   /// <summary>
   /// Get user meetings
   /// </summary>
   /// <param name="userId"></param>
   /// <param name="cancellationToken"></param>
   /// <returns></returns>
   public Task<IEnumerable<Meeting>?> GetUserMeetings(Guid userId, CancellationToken cancellationToken);
   
   /// <summary>
   /// Updates meeting status
   /// </summary>
   /// <param name="meetingId"></param>
   /// <param name="status"></param>
   /// <param name="cancellationToken"></param>
   /// <returns></returns>
   public Task<Meeting?> UpdateMeetingStatusAsync(Guid meetingId, MeetingStatus status, CancellationToken cancellationToken);
}