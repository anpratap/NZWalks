using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalksProfile:Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk,Models.DTO.Walk>();
            CreateMap<Models.Domain.Walk,Models.DTO.WalkRequest>()
                .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>();
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyRequest>()
                .ReverseMap();
        }
    }
}
