using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Common.Utilities {
    public class GameHubsConnectionUtility {
        private static readonly Dictionary<string, List<string>> OnlineAccounts = new Dictionary<string, List<string>>();
        public Task GameHubsConnected(string accountId, string connectionId) {
            lock (OnlineAccounts) {
                if (OnlineAccounts.ContainsKey(accountId)) {
                    OnlineAccounts[accountId].Add(connectionId);
                }
                else {
                    OnlineAccounts.Add(accountId, new List<string>() { connectionId });
                }
            }
            return Task.CompletedTask;
        }
        public Task GameHubsDisconnected(string accountId, string connectionId) {
            lock (OnlineAccounts) {
                if (!OnlineAccounts.ContainsKey(accountId)) return Task.CompletedTask;
                OnlineAccounts[accountId].Remove(connectionId);
                if (OnlineAccounts[accountId].Count == 0) {
                    OnlineAccounts.Remove(accountId);
                }

            }
            return Task.CompletedTask;
        }
        
    }
}
