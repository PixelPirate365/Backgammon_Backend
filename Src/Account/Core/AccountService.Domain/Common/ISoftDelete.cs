﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Common {
    public interface ISoftDelete {
        [DefaultValue(false)]
        bool IsDeleted { get; set; }
    }
}
