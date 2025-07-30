using AutoMapper;
using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Application.Interfaces;
using KitchenEquipmentManagement.Application.Interfaces.Persistence;
using KitchenEquipmentManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitchenEquipmentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public SiteController(ISiteService siteService, IMapper mapper)
        {
            _siteService = siteService;
            _mapper = mapper;
        }

        // GET: api/site
        [HttpGet]
        public async Task<IActionResult> GetAllSites()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var sites = await _siteService.GetAllSitesByUserIdAsync(userId);
            return Ok(sites);
        }

        // GET: api/site/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSite(int id)
        {
            var site = await _siteService.GetSiteAsync(id);
            if (site == null)
                return NotFound();

            return Ok(site);
        }

        // POST: api/site
        [HttpPost]
        public async Task<IActionResult> AddSite([FromBody] SiteDto siteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = int.Parse(User.FindFirst("id").Value);
            var site = _mapper.Map<Site>(siteDto);
            site.UserId = userId;
            var result = await _siteService.AddSiteAsync(site);

            if (!result)
                return BadRequest("Adding Site failed");

            return Ok($"Site {site.Description} created successfully.");
        }

        // DELETE: api/site/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            var existingSite = await _siteService.GetSiteAsync(id);
            if (existingSite == null)
                return NotFound();

            var result = await _siteService.DeleteSiteAsync(id);
            if (!result)
                return BadRequest($"{existingSite.Description} Failed.");
            return Ok($"Site {existingSite.Description} is deleted.");
        }

        // PUT: api/site/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSite(int id, [FromBody] SiteDto siteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSite = await _siteService.GetSiteAsync(id);
            if (existingSite == null)
                return NotFound();

            var updatedSite = _mapper.Map(siteDto, existingSite);
            await _siteService.UpdateSiteAsync(updatedSite);

            return Ok($"Site {updatedSite.Description} is updated.");
        }
    }
}
