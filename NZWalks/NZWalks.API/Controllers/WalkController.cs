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
        private readonly IMapper _mapper;
        public WalkController(IWalksRepository walksRepository, IMapper mapper)
        {
            _walksRepository = walksRepository;
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
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.WalkRequest region)
        {
            if (region == null)
            {
                throw new ArgumentException("ERROR: Model cannot be empty.");
            }
            var request = _mapper.Map<Models.Domain.Walk>(region);
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
            if (walk == null)
            {
                throw new ArgumentException("ERROR: Model cannot be empty.");
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
    }
}
