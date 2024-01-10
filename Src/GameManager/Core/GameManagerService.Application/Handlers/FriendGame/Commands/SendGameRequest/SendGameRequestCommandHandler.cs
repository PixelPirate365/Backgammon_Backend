using AutoMapper;
using GameManagerService.Application.Interfaces;
using GameManagerService.Application.Interfaces.Repository;
using GameManagerService.Common.Constants;
using GameManagerService.Common.EventModels;
using GameManagerService.Common.Responses;
using GameManagerService.Domain.Entities;
using GameManagerService.MessageBus.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest {
    public class SendGameRequestCommandHandler : IRequestHandler<SendGameRequestCommand, Response> {
        readonly ILogger<SendGameRequestCommandHandler> _logger;
        readonly ICurrentUserService _currentUserService;
        readonly IRepository<FriendGameRequest> _friendGameRequestRepository;
        readonly IRepository<Player> _playerRepository;
        readonly IMapper _mapper;
        readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public SendGameRequestCommandHandler(
            ILogger<SendGameRequestCommandHandler> logger,
            ICurrentUserService currentUserService,
            IRepository<FriendGameRequest> friendGameRequestRepository,
            IMapper mapper,
            IRepository<Player> playerRepository,
            IRabbitMQMessageSender rabbitMQMessageSender) {
            _currentUserService = currentUserService;
            _logger = logger;
            _friendGameRequestRepository = friendGameRequestRepository;
            _mapper = mapper;
            _playerRepository = playerRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;

        }

        public async Task<Response> Handle(SendGameRequestCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(SendGameRequestCommandHandler)}");

            var map = _mapper.Map<CheckRecieverBalanceModel>(request);
            map.SenderId = _currentUserService.UserId;
            _rabbitMQMessageSender.SendMessage(map, EventNameConstants.CheckRecieverBalanceEvent);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(SendGameRequestCommandHandler)}");
            return new Response { Successful = true, Message = MessageConstants.GameFriendRequestMessage };
        }
    }
}
