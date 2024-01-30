using GameManagerService.Application.Interfaces.Repository;
using GameManagerService.Application.Services.SignalRSender;
using GameManagerService.Common.Dtos;
using GameManagerService.Common.Responses;
using GameManagerService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameManagerService.Application.Handlers.Players.Queries.GetOpponentPlayerBalance {
    public class GetOpponentPlayerQueryHandler : IRequestHandler<GetOpponentPlayerQuery, bool> {
        readonly ILogger<GetOpponentPlayerQueryHandler> _logger;
        readonly IRepository<FriendGameRequest> _friendGameRepository;
        readonly ISignalRMessageSender _signalRMessageSender;

        public GetOpponentPlayerQueryHandler(
            ILogger<GetOpponentPlayerQueryHandler> logger,
            IRepository<FriendGameRequest> friendGameRepository,
            ISignalRMessageSender signalRMessageSender
        ) {
            _logger = logger;
            _friendGameRepository = friendGameRepository;
            _signalRMessageSender = signalRMessageSender;
        }

        public async Task<bool> Handle(
            GetOpponentPlayerQuery request,
            CancellationToken cancellationToken
        ) {
            _logger.LogInformation(
                $"{nameof(Handle)} method running in Handler: {nameof(GetOpponentPlayerQueryHandler)}"
            );
            if (request.BalanceAmount < request.BetAmount) {
                await _signalRMessageSender.NotifySenderRecieverBalanceNotAvailable(
                    new Response<ParentMessageDto<bool>> {
                        Message = "Balance is not available", // add as constant later
                        Result = new ParentMessageDto<bool> { Message = false, UserId = request.SenderId }
                    }
                );
            }
            else {
                await _signalRMessageSender.NotifySenderRecieverBalanceAvailable(
                    new Response<ParentMessageDto<SenderRecieverGameBootstrapDto>>() {
                        Message = "Balance is available",
                        Result = new ParentMessageDto<SenderRecieverGameBootstrapDto> {
                            Message = new SenderRecieverGameBootstrapDto {
                                RecieverId = request.RecieverId,
                                SenderId = request.SenderId
                            },
                            UserId = request.SenderId
                        }
                    });


            }

            _logger.LogInformation(
                $"{nameof(Handle)} method completed in Handler: {nameof(GetOpponentPlayerQueryHandler)}"
            );
            return true;
        }
    }
}
