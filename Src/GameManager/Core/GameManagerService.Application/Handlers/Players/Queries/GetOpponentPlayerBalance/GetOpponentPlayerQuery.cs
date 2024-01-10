using MediatR;

namespace GameManagerService.Application.Handlers.Players.Queries.GetOpponentPlayerBalance {
    public class GetOpponentPlayerQuery : IRequest<bool> {
        public Guid RecieverId { get; set; }
        public Guid SenderId { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal BetAmount { get; set; }
    }
}
