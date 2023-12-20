using AccountService.Common.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace AccountService.Application.Handlers.Account.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommand : IRequest<UpdateAccountResponse>
    {
        public string? Nickname { get; set; }
        public string? ProfileDescription { get; set; }
        [JsonIgnore]
        public string? ImagePath { get; set; }
        public string? Image { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
