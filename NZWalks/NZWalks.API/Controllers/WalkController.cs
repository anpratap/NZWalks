using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositiories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;
        public WalkController(IWalksRepository walksRepository, IRegionRepository regionRepository, 
            IWalkDifficultyRepository difficultyRepository, IMapper mapper)
        {
            _walksRepository = walksRepository;
            _regionRepository = regionRepository;   
            _difficultyRepository = difficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _walksRepository.GetAllAsync();
            return Ok(_mapper.Map<List<Models.DTO.Walk>>(result));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalksAsync")]
        public async Task<IActionResult> GetWalksAsync([FromRoute] Guid id)
        {
            var result = await _walksRepository.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Models.DTO.Walk>(result));
        }

        [HttpPost]
        [ActionName("AddWalkAsync")]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.WalkRequest model)
        {
            if (!(await ValidateWalkAsync(model)))
            {
                return BadRequest(ModelState);
            }
            var request = _mapper.Map<Models.Domain.Walk>(model);
            request.Id = Guid.NewGuid();
            var result = await _walksRepository.AddAsync(request);
            if (result == null)
            {
                return NoContent();
            }
            return CreatedAtAction(nameof(GetWalksAsync), new { id = result.Id }, _mapper.Map<Models.DTO.Walk>(result));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _walksRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdateWalkAsync/{id}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.WalkRequest walk)
        {
            if (!(await ValidateWalkAsync(walk)))
            {
                return BadRequest(ModelState);
            }
            var request = _mapper.Map<Models.Domain.Walk>(walk);
            request.Id = id;
            var result = await _walksRepository.UpdateAsync(id, request);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<Models.DTO.Walk>(result));
        }

        private async Task<bool> ValidateWalkAsync(Models.DTO.WalkRequest model)
        {
            if (model == null)
            {
                ModelState.AddModelError(nameof(model), $"{nameof(model)} canot be null or empty, it is required.");
            }
            if (string.IsNullOrWhiteSpace(model?.Name))
            {
                ModelState.AddModelError(nameof(model.Name), $"{nameof(model.Name)} canot be null or empty.");
            }

            if (model?.Length <= 0)
            {
                ModelState.AddModelError(nameof(model.Length), $"{nameof(model.Length)} canot be less or equal to zero.");
            }

            var region = await _regionRepository.GetAsync(model.RegionId);
            if (region==null)
            {
                ModelState.AddModelError(nameof(region), $"{nameof(region)} is invalid.");
            }

            var walkDifficulty = await _difficultyRepository.GetAsync(model.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(walkDifficulty), $"{nameof(walkDifficulty)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
    }
}
