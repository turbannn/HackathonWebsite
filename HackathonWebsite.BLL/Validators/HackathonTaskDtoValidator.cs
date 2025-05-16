using FluentValidation;
using HackathonWebsite.BLL.Interfaces;

namespace HackathonWebsite.BLL.Validators
{
    public class HackathonTaskDtoValidator : AbstractValidator<ITaskTransferObject>
    {
        public HackathonTaskDtoValidator()
        {
            //int
            RuleFor(h => h.Id)
                .GreaterThan(-1).WithMessage("Id is less than 0");

            RuleFor(h => h.Rating)
                .GreaterThan(-1).WithMessage("Error: Rating less than 0")
                .LessThan(101).WithMessage("Error: Rating is more than 100");

            //str
            RuleFor(h => h.Description)
                .MaximumLength(100).WithMessage("Description is too long")
                .NotEmpty().WithMessage("Description empty error");

            RuleFor(h => h.Name)
                .MaximumLength(40).WithMessage("Name is too long")
                .NotEmpty().WithMessage("Name empty error");
        }
    }
}
