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
        public async Task<IActionResult> GetAllRegions()
        {
            var result = await _regionRepository.GetAllAsync();
            return Ok(_autoMapper.Map<List<Models.DTO.Region>>(result));
        }
    }
}
