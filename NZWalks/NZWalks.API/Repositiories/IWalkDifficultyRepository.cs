using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<WalkDifficulty> AddAsync(WalkDifficulty model);
        Task<WalkDifficulty> UpdateAsync(Guid regionId, WalkDifficulty model);
    }
}
