namespace Auth.Server.Interfaces {
    public interface IHasModificationTime {
        DateTime? ModifiedDate { get; set; }
    }
}
