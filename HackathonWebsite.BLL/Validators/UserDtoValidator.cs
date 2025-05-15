using FluentValidation;
using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.Validators
{
    public class UserDtoValidator : AbstractValidator<IUserTransferObject>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.Id)
                .GreaterThan(-1).WithMessage("Id is less than 0");

            RuleFor(u => u.Rating)
                .GreaterThan(-1).WithMessage("Rating is less than 0");

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("User Username is empty")
                .MaximumLength(40).WithMessage("User Username maximum length exceeded");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("User Role is empty")
                .MaximumLength(10).WithMessage("User Role maximum length exceeded");
        }
    }
}
