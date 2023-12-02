namespace AccountService.Common.Utilities {
    public class PresenceUtility {
        private static readonly Dictionary<string, List<string>> OnlineAccounts = new Dictionary<string, List<string>>();
        public static Task AccountConnected(string accountId, string connectionId) {
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
        public static Task AccountDisconnected(string accountId, string connectionId) {
            lock (OnlineAccounts) {
                if (!OnlineAccounts.ContainsKey(accountId)) return Task.CompletedTask;
                OnlineAccounts[accountId].Remove(connectionId);
                if (OnlineAccounts[accountId].Count == 0) {
                    OnlineAccounts.Remove(accountId);
                }

            }
            return Task.CompletedTask;
        }
        public static Dictionary<string, List<string>> GetOnlineAccounts() {
            return OnlineAccounts;
        }
    }
}
