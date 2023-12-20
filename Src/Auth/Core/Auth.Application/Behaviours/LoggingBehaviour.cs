using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Behaviours {
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger) {
            _logger = logger;
        }
        public async Task Process(TRequest request, CancellationToken cancellationToken) {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("AuthService Request: {Name} {@Request}",
                 requestName, request);
        }
    }
}
