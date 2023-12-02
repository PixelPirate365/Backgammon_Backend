using AutoMapper;
using GameManagerService.Application.Interfaces;
using GameManagerService.Application.Interfaces.Repository;
using GameManagerService.Common.Responses;
using GameManagerService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest {
    public class SendGameRequestCommandHandler : IRequestHandler<SendGameRequestCommand, Response> {
        readonly ILogger<SendGameRequestCommandHandler> _logger;
        readonly ICurrentUserService _currentUserService;
        readonly IRepository<FriendGameRequest> _friendGameRequestRepository;
        readonly IMapper _mapper;
        
        public SendGameRequestCommandHandler(
            ILogger<SendGameRequestCommandHandler> logger,
            ICurrentUserService currentUserService,
            IRepository<FriendGameRequest> friendGameRequestRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _logger = logger;
            _friendGameRequestRepository = friendGameRequestRepository;
            _mapper = mapper;
        
        }

        public Task<Response> Handle(SendGameRequestCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(SendGameRequestCommandHandler)}");



            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(SendGameRequestCommandHandler)}");

            throw new NotImplementedException();
        }
    }
}
