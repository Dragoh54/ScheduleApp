using MeetingService.Api.Interfaces;

namespace MeetingService.Api.Managers;

public class UserConnectionManager : IUserConnectionManager
{
    private static readonly Dictionary<string, List<string>> UserConnectionMap = new();
    private static readonly string UserConnectionMapLocker = string.Empty;
    
    public void KeepUserConnection(string userId, string connectionId)
    {
        lock (UserConnectionMapLocker)
        {
            if (!UserConnectionMap.ContainsKey(userId))
            {
                UserConnectionMap[userId] = [];
            }
            UserConnectionMap[userId].Add(connectionId);
        }
    }

    public void RemoveUserConnection(string connectionId)
    {
        lock (UserConnectionMapLocker)
        {
            var userIds = UserConnectionMap.Keys
                .Where(userId => UserConnectionMap.ContainsKey(userId))
                .Where(userId => UserConnectionMap[userId].Contains(connectionId));
            
            foreach (var userId in userIds)
            {
                UserConnectionMap[userId].Remove(connectionId);
                break;
            }
        }
    }

    public List<string> GetUserConnections(string userId)
    {
        List<string> conn;
        lock (UserConnectionMapLocker)
        {
            conn = UserConnectionMap[userId];
        }
        return conn;
    }
}