using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Handlers.User.Commands.CreateUserProfile
{
    public class CreateUserProfileCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public string Nickname { get; set; }

    }
}
