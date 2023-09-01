using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Commands {
    public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, bool> {
        ILogger<SendFriendRequestCommandHandler> _logger;
        IRepository<FriendRequest> repository;
        IMapper _mapper;
        public SendFriendRequestCommandHandler(ILogger<SendFriendRequestCommandHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            
        }
        public Task<bool> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken) {

            

            throw new NotImplementedException();
        }
    }
}
