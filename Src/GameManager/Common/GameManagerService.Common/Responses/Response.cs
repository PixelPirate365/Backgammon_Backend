using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Common.Responses {
    public class Response<T> : Response {
        public T? Result { get; set; }
    }
    public class Response {
        public bool Successful { get; set; }
        public string? Message { get; set; }
    }
}
