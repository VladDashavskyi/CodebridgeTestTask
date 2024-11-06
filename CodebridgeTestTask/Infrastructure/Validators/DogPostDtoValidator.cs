using CodebridgeTestTask.Common.DTO;
using FluentValidation;

namespace CodebridgeTestTask.Infrastructure.Validators
{
    public class DogPostDtoValidator : AbstractValidator<DogDto>
    {
        /// <summary>
        /// Provide validation for ChangeRequestItemCreateDto.
        /// </summary>
        public DogPostDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull()
               .MaximumLength(25);

            RuleFor(x => x.Color)
               .NotEmpty()
               .NotNull()
               .MaximumLength(50);

            RuleFor(x => x.TailLength)
                  .NotNull()
                  .GreaterThan(0)
                  .WithMessage("Tail height is a negative number or is not a number.");

            RuleFor(x => x.Weight)
                  .NotNull()
                  .GreaterThan(0)
                  .WithMessage("Weight is a negative number or is not a number.");
        }
    }
}
