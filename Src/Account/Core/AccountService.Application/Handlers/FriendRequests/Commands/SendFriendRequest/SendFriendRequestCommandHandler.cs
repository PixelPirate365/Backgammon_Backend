using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Handlers.Account.Queries;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Commands.SendFriendRequest
{
    public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, bool>
    {
        readonly ILogger<SendFriendRequestCommandHandler> _logger;
        readonly IRepository<FriendRequest> _repository;
        readonly IMapper _mapper;
        public SendFriendRequestCommandHandler(ILogger<SendFriendRequestCommandHandler> logger,
            IMapper mapper,
            IRepository<FriendRequest> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;

        }
        public async Task<bool> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(SendFriendRequestCommandHandler)}");
            var friendRequest = _mapper.Map<FriendRequest>(request);
            friendRequest.SenderProfileId = Guid.Parse("E9A4B4BE-62CA-4472-AF9D-00DDD8FF0E7C"); // hard coded
            await _repository.Add(friendRequest);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(SendFriendRequestCommandHandler)}");
            //D895BED5-FB48-42AC-BB26-D1B72324AE44
            return true;
        }
    }
}
