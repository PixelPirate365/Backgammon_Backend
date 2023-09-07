using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Common.Options.Swagger {
    public class SwaggerOptions {
        public string JsonRoute { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string UiEndPoint { get; set; } = null!;
    }
}
