using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalksRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            await _nZWalksDbContext.Walks.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task DeleteAsync(Guid id)
        {
            var obj = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(i => i.Id == id);
            if (obj != null)
            {
                _nZWalksDbContext.Walks.Remove(obj);
                await _nZWalksDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            //this is en example of eager loading to load related entities as well
            return await _nZWalksDbContext
                .Walks
                .Include(i=>i.Region)
                .Include(i=>i.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            var result= await _nZWalksDbContext
                .Walks
                .Include(i => i.Region)
                .Include(i => i.WalkDifficulty)
                .FirstOrDefaultAsync(i => i.Id == id);
            if(result != null)
            {
                return result;
            }
            return new Walk();
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var obj = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(i => i.Id == id);
            if (obj != null)
            {
                obj.Name = walk.Name;
                obj.Length = walk.Length;
                obj.RegionId = walk.RegionId;
                obj.WalkDifficultyId = walk.WalkDifficultyId;
                await _nZWalksDbContext.SaveChangesAsync();
            }
            return walk;
        }
    }
}
