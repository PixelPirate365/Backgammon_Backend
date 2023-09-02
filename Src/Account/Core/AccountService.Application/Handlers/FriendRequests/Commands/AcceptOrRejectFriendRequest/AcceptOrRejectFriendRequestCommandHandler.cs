using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Common.Constants;
using AccountService.Common.Enums;
using AccountService.Common.Responses;
using AccountService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.FriendRequests.Commands.AcceptOrRejectFriendRequest {
    public class AcceptOrRejectFriendRequestCommandHandler : IRequestHandler<AcceptOrRejectFriendRequestCommand, Response> {
        readonly ILogger<AcceptOrRejectFriendRequestCommandHandler> _logger;
        readonly IRepository<FriendRequest> _repository;
        public AcceptOrRejectFriendRequestCommandHandler(ILogger<AcceptOrRejectFriendRequestCommandHandler> logger,
            IRepository<FriendRequest> repository) {
            _logger = logger;
            _repository = repository;
        }
        public async Task<Response> Handle(AcceptOrRejectFriendRequestCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(AcceptOrRejectFriendRequestCommandHandler)}");
            var friendRequest = await _repository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == request.FriendRequestId);
            friendRequest.Status = (int)request.Status;
            await _repository.Update(friendRequest);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(AcceptOrRejectFriendRequestCommandHandler)}");
            return new Response() {
                Successful = true,
                Message = request.Status == FriendRequestStatusEnum.Accepted ?
                ResponseMessageConstants.FriendRequestAccepted : ResponseMessageConstants.FriendRequestRejected
            };
        }

    }
}
