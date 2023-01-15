using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace NZWalks.API.Profiles
{
    /// <summary>
    /// auto mapper profile class to create map of domain model with dto model
    /// this has to be register in contaier to be used
    /// </summary>
    public class RegionsProfile:Profile
    {
        public RegionsProfile()
        {
            //ForMember() method is used in case if source (domain) and destination(dto) having different property names
            //ReverseMap() is used to convert dto back to domain model
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
                //.ForMember(dest => dest.RegionCode, options => options.MapFrom(src => src.Code));
                //.ReverseMap(); 

            CreateMap<Models.Domain.Region, Models.DTO.RegionRequest>()
                .ReverseMap();
        }
    }
}
