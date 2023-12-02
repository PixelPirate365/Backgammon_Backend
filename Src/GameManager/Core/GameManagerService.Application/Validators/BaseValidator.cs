using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Validators {
    public class BaseValidator<T> : AbstractValidator<T> {
        protected bool Validate(Guid identifier) => identifier != Guid.Empty;
    }
}
