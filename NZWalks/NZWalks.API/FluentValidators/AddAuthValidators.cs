using FluentValidation;

namespace NZWalks.API.FluentValidators
{
    public class AddAuthValidators:AbstractValidator<Models.DTO.LoginRequest>
    {
        public AddAuthValidators()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
