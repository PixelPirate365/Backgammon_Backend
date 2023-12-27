namespace Auth.Server.Interfaces {
    public interface ICreationAudited : IHasCreationTime {
        string CreatedBy { get; set; }
    }
}
