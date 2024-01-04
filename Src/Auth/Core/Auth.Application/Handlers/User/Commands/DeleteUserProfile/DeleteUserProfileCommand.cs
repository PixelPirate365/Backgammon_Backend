﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Handlers.User.Commands.DeleteUserProfile {
    public class DeleteUserProfileCommand : IRequest<Unit> {

        public Guid UserId { get; set; }

    }
}
