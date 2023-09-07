using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Interfaces;
using AccountService.Common.Enums;
using AccountService.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Handlers.FriendRequests.Queries.GetFriendsRequest {
    public class GetFriendsRequestQueryHandler : IRequestHandler<GetFriendsRequestQuery, List<GetFriendRequestResponse>> {

        readonly ILogger<GetFriendsRequestQueryHandler> _logger;
        readonly IRepository<FriendRequest> _repository;
        readonly IMapper _mapper;
        readonly ICurrentUserService _currentUserService;
        public GetFriendsRequestQueryHandler(
            ILogger<GetFriendsRequestQueryHandler> logger,
            IMapper mapper,
            IRepository<FriendRequest> repository,
            ICurrentUserService currentUserService) {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<List<GetFriendRequestResponse>> Handle(GetFriendsRequestQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetFriendsRequestQueryHandler)}");
            var result = await _repository.TableNoTracking.Include(x => x.SenderProfile)
                .Where(x => 
                x.RecieverProfileId == _currentUserService.UserId
                && x.Status == (int)FriendRequestStatusEnum.Pending)
                .ProjectTo<GetFriendRequestResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetFriendsRequestQueryHandler)}");
            return result;
        }
    }
}
