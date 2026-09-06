using Microsoft.AspNetCore.Mvc;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CfnDealerController : ControllerBase
    {
        private readonly ICfnDealerService _service;
        public CfnDealerController(ICfnDealerService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CfnDealerDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CfnDealerDto dto)
            => Ok(await _service.UpdateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _service.DeleteAsync(id));
    }
}