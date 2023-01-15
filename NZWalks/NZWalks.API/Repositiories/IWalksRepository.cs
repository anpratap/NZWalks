using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public interface IWalksRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
}
