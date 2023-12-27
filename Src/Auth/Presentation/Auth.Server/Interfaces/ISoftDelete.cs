using System.ComponentModel;

namespace Auth.Server.Interfaces {
    public interface ISoftDelete {
        [DefaultValue(false)]
        bool IsDeleted { get; set; }
    }
}
