﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Common {
    public interface ICreationAudited : IHasCreationTime {
        string? CreatedBy { get; set; }
    }
}
