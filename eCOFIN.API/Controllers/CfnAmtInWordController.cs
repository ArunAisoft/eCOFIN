using Microsoft.AspNetCore.Mvc;
using eCOFIN.Application.DTOs;
using eCOFIN.Application.Interfaces;

namespace eCOFIN.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CfnAmtInWordController : ControllerBase
    {
        private readonly ICfnAmtInWordService _service;
        public CfnAmtInWordController(ICfnAmtInWordService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CfnAmtInWordDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CfnAmtInWordDto dto)
            => Ok(await _service.UpdateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(decimal id)
            => Ok(await _service.DeleteAsync(id));
    }
}