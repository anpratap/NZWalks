using FluentValidation;

namespace NZWalks.API.FluentValidators
{
    public class AddRegionRequestValidators:AbstractValidator<Models.DTO.RegionRequest>
    {
        public AddRegionRequestValidators()
        {
            RuleFor(i=>i.Code).NotEmpty();
            RuleFor(i=>i.Name).NotEmpty();
            RuleFor(i => i.Area).GreaterThan(0);
            RuleFor(i => i.Lat).GreaterThan(0);
            RuleFor(i => i.Long).GreaterThan(0);
            RuleFor(i => i.Population).GreaterThan(0);
        }
    }
}
