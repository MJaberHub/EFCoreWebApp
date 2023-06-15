using EFCoreWebApp.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Numerics;

namespace EFCoreWebApp.Validator
{
    public class AddCustomerValidator : AbstractValidator<CustomerRequest>
    {
        public AddCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please specify a First Name");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a Last Name");
        }
    }
}
