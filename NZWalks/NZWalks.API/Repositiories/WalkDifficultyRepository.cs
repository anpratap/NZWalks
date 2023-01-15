using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
        _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty model)
        {
            await _nZWalksDbContext.WalkDifficulties.AddAsync(model);
            await _nZWalksDbContext.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(Guid id)
        {
            var obj = await _nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(i => i.Id == id);
            if(obj!= null)
            {
                _nZWalksDbContext.WalkDifficulties.Remove(obj);
               await _nZWalksDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _nZWalksDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await _nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty model)
        {
            var obj = await _nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(i => i.Id == id);
            if (obj != null)
            {
               obj.Code = model.Code;
                await _nZWalksDbContext.SaveChangesAsync();
            }
            return model;
        }
    }
}
