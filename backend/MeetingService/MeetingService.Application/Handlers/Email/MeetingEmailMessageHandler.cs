using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Handlers.Email;

public static class MeetingEmailMessageHandler
{
    public static string MeetingDeletedBody(string meetingTitle) => 
        $"Meeting {meetingTitle} was deleted!";


    public static string MeetingRescheduledBody(string meetingTitle, DateTime newStartTime, DateTime newEndTime) =>
        $"""
         Meeting {meetingTitle} was rescheduled! 
         Start time: {newStartTime},
         End time: {newEndTime}
         """;

    public static string MeetingInformationUpdatedBody(string oldTitle, string updatedTitle, string updatedDescription) =>
        $"""
         Meeting {oldTitle} was updated! 
         Title: {updatedTitle},
         Description: {updatedDescription}
         """;

    public static string MeetingStatusUpdatedBody(string meetingTitle, MeetingStatus updatedMeetingStatus) =>
        $"""
         Meeting {meetingTitle} was updated! 
         Status: {updatedMeetingStatus}
         """;
}