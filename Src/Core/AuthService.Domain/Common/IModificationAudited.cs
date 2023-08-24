using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Common {
    public interface IModificationAudited : IHasModificationTime {
        string? ModifiedBy { get; set; }
    }
}
