using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext= nZWalksDbContext ?? throw new ArgumentNullException(nameof(nZWalksDbContext));
        }

        public async Task<Region> AddAsync(Region region)
        {
            await _nZWalksDbContext.Regions.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task DeleteAsync(Guid regionId)
        {
           var region= await _nZWalksDbContext.Regions.FindAsync(regionId);
            if(region != null)
            {
                _nZWalksDbContext.Regions.Remove(region);
                await _nZWalksDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid regionId)
        {
            return await _nZWalksDbContext.Regions.FirstOrDefaultAsync(i => i.Id == regionId);
        }

        public async Task<Region> UpdateAsync(Guid regionId, Region region)
        {
            var regionObj = await _nZWalksDbContext.Regions.FindAsync(regionId);
            if (regionObj != null)
            {
                regionObj.Area = region.Area;
                regionObj.Name = region.Name;
                regionObj.Code = region.Code;
                regionObj.Lat = region.Lat;
                regionObj.Long = region.Long;
                regionObj.Population = region.Population;
                await _nZWalksDbContext.SaveChangesAsync();
            }
            return region;
        }
    }
}
