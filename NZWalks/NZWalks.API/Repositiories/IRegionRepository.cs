using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
