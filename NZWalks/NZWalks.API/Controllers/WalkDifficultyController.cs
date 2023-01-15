using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositiories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDiffRepository;
        private readonly IMapper _mapper;
        public WalkDifficultyController(IWalkDifficultyRepository walkDiffRepository, IMapper mapper)
        {
            _walkDiffRepository = walkDiffRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _walkDiffRepository.GetAllAsync();
            return Ok(_mapper.Map<List<Models.DTO.Walk>>(result));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync([FromRoute] Guid id)
        {
            var result = await _walkDiffRepository.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Models.DTO.WalkDifficulty>(result));
        }

        [HttpPost]
        [ActionName("AddWalkDifficultyAsync")]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.WalkDifficultyRequest model)
        {
            if (model == null)
            {
                throw new ArgumentException("ERROR: Model cannot be empty.");
            }
            var request = _mapper.Map<Models.Domain.WalkDifficulty>(model);
            request.Id = Guid.NewGuid();
            var result = await _walkDiffRepository.AddAsync(request);
            if (result == null)
            {
                return NoContent();
            }
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = result.Id }, _mapper.Map<Models.DTO.Walk>(result));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _walkDiffRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ActionName("UpdateWalkDifficultyAsync/{id}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.WalkDifficultyRequest walk)
        {
            if (walk == null)
            {
                throw new ArgumentException("ERROR: Model cannot be empty.");
            }
            var request = _mapper.Map<Models.Domain.WalkDifficulty>(walk);
            request.Id = id;
            var result = await _walkDiffRepository.UpdateAsync(id, request);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<Models.DTO.WalkDifficulty>(result));
        }
    }
}
