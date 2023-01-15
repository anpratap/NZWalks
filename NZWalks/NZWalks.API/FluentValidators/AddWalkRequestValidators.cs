using FluentValidation;

namespace NZWalks.API.FluentValidators
{
    public class AddWalkRequestValidators :AbstractValidator<Models.DTO.WalkRequest>
    {
        public AddWalkRequestValidators()
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i => i.Length).GreaterThan(0);
        }
    }
}
