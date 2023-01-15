using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
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
            var result = await _regionRepository.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_autoMapper.Map<Models.DTO.Region>(result));
        }

        /// <summary>
        /// This method has used Fluent Validation on passed model, so if there is an error control will not get inside the method
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddRegionAsync")]
        public async Task<IActionResult> AddRegionAsync([FromBody] Models.DTO.RegionRequest region)
        {
            //.NET validation code
            //ModelState is to be passed back to client to show error messages.
            //if (!ValidateRegionAsync(region))
            //{
            //    return BadRequest(ModelState);
            //}
            var request = _autoMapper.Map<Models.Domain.Region>(region);
            request.Id = Guid.NewGuid();
            var result = await _regionRepository.AddAsync(request);
            if (result == null)
            {
                return NoContent();
            }
            return CreatedAtAction(nameof(GetRegionAsync), new { id = result.Id }, _autoMapper.Map<Models.DTO.Region>(result));
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
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.RegionRequest region)
        {
            if (!ValidateRegionAsync(region))
            {
                return BadRequest(ModelState);
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

        private bool ValidateRegionAsync(Models.DTO.RegionRequest model)
        {
            if (model == null)
            {
                ModelState.AddModelError(nameof(model), $"{nameof(model)} canot be null or empty, it is required.");
            }
            if (string.IsNullOrWhiteSpace(model?.Name))
            {
                ModelState.AddModelError(nameof(model.Name), $"{nameof(model.Name)} canot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(model?.Code))
            {
                ModelState.AddModelError(nameof(model.Code), $"{nameof(model.Code)} canot be null or empty.");
            }
            if (model?.Area <= 0)
            {
                ModelState.AddModelError(nameof(model.Area), $"{nameof(model.Area)} canot be less or equal to zero.");
            }
            if (model?.Lat <= 0)
            {
                ModelState.AddModelError(nameof(model.Lat), $"{nameof(model.Lat)} canot be less or equal to zero.");
            }
            if (model?.Long <= 0)
            {
                ModelState.AddModelError(nameof(model.Long), $"{nameof(model.Long)} canot be less or equal to zero.");
            }
            if (model?.Population <= 0)
            {
                ModelState.AddModelError(nameof(model.Population), $"{nameof(model.Population)} canot be less or equal to zero.");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
    }
}
