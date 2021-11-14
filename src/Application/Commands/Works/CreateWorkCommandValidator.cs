using FluentValidation;

namespace Application.Commands.Works
{
    public sealed class CreateWorkCommandValidator : AbstractValidator<CreateWorkCommand>
    {
        public CreateWorkCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(100);
            
            RuleFor(c => c.Description)
                .NotEmpty()
                .MaximumLength(65535);

            // todo: add validation for date range
            
            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .MinimumLength(12) // with no formatting
                .MaximumLength(30); // with the most pretty formatting
            
            // todo: add validation for cost range

            RuleFor(c => c.CategoryId)
                .GreaterThanOrEqualTo(1L);
            
            RuleFor(c => c.SubcategoryId)
                .GreaterThanOrEqualTo(1L);
        }
    }
}
