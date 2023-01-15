using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid regionId);
        Task DeleteAsync(Guid regionId);
        Task<Region> AddAsync(Region region);
        Task<Region> UpdateAsync(Guid regionId, Region region);
    }
}
