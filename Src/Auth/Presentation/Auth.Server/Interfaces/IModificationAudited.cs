namespace Auth.Server.Interfaces {
    public interface IModificationAudited : IHasModificationTime {
        string ModifiedBy { get; set; }
    }
}
