namespace AccountService.Application.Handlers.Friends.Queries.GetFriends {
    public class GetFriendResponse {
        public List<Guid> OnlineFriendIds { get; set; }
        public GetLoggedInProfile GetLoggedInProfile { get; set; }
    }
    public class GetLoggedInProfile {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string Nickname { get; set; }
    }
}
