using FluentValidation;

namespace NZWalks.API.FluentValidators
{
    public class AddWalkDifficultyRequestValidators :AbstractValidator<Models.DTO.WalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidators()
        {
            RuleFor(i => i.Code).NotEmpty();
        }
    }
}
