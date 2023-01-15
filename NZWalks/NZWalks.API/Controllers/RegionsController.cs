using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositiories;

namespace NZWalks.API.Controllers
{
    /// <summary>
    /// ApiController attribute tells compiler that this controller is API controller not MVC
    /// </summary>
    [ApiController] 
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _autoMapper;
        public RegionsController(IRegionRepository regionRepository, IMapper autoMapper)
        {
            _regionRepository = regionRepository;
            _autoMapper = autoMapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var result = await _regionRepository.GetAllAsync();
            return Ok(_autoMapper.Map<List<Models.DTO.Region>>(result));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync([FromRoute] Guid id)
        {
            var result= await _regionRepository.GetAsync(id);
            if (result==null)
            {
                return NotFound();
            }
            return Ok(_autoMapper.Map<Models.DTO.Region>(result));
        }

        [HttpPost]
        [ActionName("AddRegionAsync")]
        public async Task<IActionResult> AddRegionAsync([FromBody] Models.DTO.RegionRequest region)
        {
            if (region == null)
            {
                throw new ArgumentException("ERROR: Model cannot be empty.");
            }
            var request = _autoMapper.Map<Models.Domain.Region>(region);
            request.Id = Guid.NewGuid();
            var result = await _regionRepository.AddAsync(request);
            if (result == null)
            {
                return NoContent();
            }
            return CreatedAtAction(nameof(GetRegionAsync),new {id=result.Id}, _autoMapper.Map<Models.DTO.Region>(result));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
           await _regionRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdateRegionAsync/{id}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] Models.DTO.RegionRequest region)
        {
            if (region == null)
            {
                throw new ArgumentException("ERROR: Model cannot be empty.");
            }
            var request = _autoMapper.Map<Models.Domain.Region>(region);
            request.Id = id;
            var result = await _regionRepository.UpdateAsync(id, request);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(_autoMapper.Map<Models.DTO.Region>(result));
        }
    }
}
