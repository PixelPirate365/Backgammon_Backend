using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Common.Utilities {
    public class GameHubsConnectionUtility {
        public static Dictionary<string, List<string>> OnlineUsers = new Dictionary<string, List<string>>();
        public static bool HasUserConnections(string userId, string connectionId) {
            try {
                if(OnlineUsers.ContainsKey(userId)) {
                    return OnlineUsers[userId].Any(x =>
                    x.Contains(connectionId));
                }
            }
            catch(Exception ex) {
                
            }
            return false;
            
        }
        public static Task GameHubsConnected(string userId, string connectionId) {
            lock (OnlineUsers) {
                if (OnlineUsers.ContainsKey(userId)) {
                    OnlineUsers[userId].Add(connectionId);
                }
                else {
                    OnlineUsers.Add(userId, new List<string>() { connectionId });
                }
            }
            return Task.CompletedTask;
        }
        public static Task GameHubsDisconnected(string userId, string connectionId) {
            lock (OnlineUsers) {
                if (!OnlineUsers.ContainsKey(userId)) return Task.CompletedTask;
                OnlineUsers[userId].Remove(connectionId);
                if (OnlineUsers[userId].Count == 0) {
                    OnlineUsers.Remove(userId);
                }

            }
            return Task.CompletedTask;
        }

    }
}
