using AccountService.Common.Enums;
using MediatR;

namespace AccountService.Application.Handlers.Account.Commands
{
    public class UpdateAccountProfileCommand : IRequest<UpdateAccountResponse> {
        public string? Nickname { get; set; }
        public string? ProfileDescription { get; set; }
        public string? Image { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
