using AccountService.Common.Enums;

namespace AccountService.Application.Handlers.Account.Commands
{
    public class UpdateAccountCommandResponse {
        public string? Nickname { get; set; }
        public string? ProfileDescription { get; set; }
        public string? Image { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
