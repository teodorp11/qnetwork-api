using Microsoft.AspNetCore.Mvc;
using qnetwork_api.DTOs.IndustrialActuators;
using qnetwork_api.Services.IndustrialDevices;

namespace qnetwork_api.Controllers.Devices
{
    [ApiController]
    [Route("api/devices/actuators")]
    public class IndustrialActuatorsController : ControllerBase
    {
        private readonly IIndustrialActuatorService _svc;

        public IndustrialActuatorsController(IIndustrialActuatorService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIndustrialActuatorDTO dto)
        {
            try
            {
                var created = await _svc.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.ID }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIndustrialActuatorDTO dto)
        {
            var ok = await _svc.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _svc.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
