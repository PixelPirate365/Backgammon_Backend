﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Exceptions {
    public class ForbiddenAccessException : Exception {
        public ForbiddenAccessException() : base() { }
    }
}
