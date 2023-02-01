using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Models.Domain.User, Models.DTO.User>()
                .ReverseMap();
        }
    }
}
