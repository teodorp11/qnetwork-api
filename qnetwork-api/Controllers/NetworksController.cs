using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qnetwork_api.DTOs.Networks;
using qnetwork_api.Services;

namespace qnetwork_api.Controllers
{
    [ApiController]
    [Route("api/networks")]
    public class NetworksController : ControllerBase
    {
        private readonly INetworkService _svc;

        public NetworksController(INetworkService svc)
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
        public async Task<IActionResult> Create([FromBody] CreateNetworkDTO dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNetworkDTO dto)
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

        [HttpGet("{networkId:guid}/devices")]
        public async Task<IActionResult> GetAllDevices(Guid networkId)
        {
            var devices = await _svc.GetAllDevicesAsync(networkId);
            return Ok(devices);
        }


        [HttpGet("{networkId:guid}/devices/{deviceId:guid}")]
        public async Task<IActionResult> GetDeviceById(Guid networkId, Guid deviceId)
        {
            // Try to get the device
            var device = await _svc.GetDeviceByIdAsync(networkId, deviceId);

            // Return informative messages for 404
            if (device == null)
            {
                if (!await _svc.NetworkExistsAsync(networkId))
                    return NotFound(new { message = $"Network with ID '{networkId}' does not exist." });

                if (!await _svc.DeviceExistsAsync(deviceId))
                    return NotFound(new { message = $"Device with ID '{deviceId}' does not exist." });

                return NotFound(new { message = $"Device '{deviceId}' is not attached to network '{networkId}'." });
            }


            return Ok(device);
        }



        [HttpPost("{networkId:guid}/devices/{deviceId:guid}")]
        public async Task<IActionResult> AttachDevice(Guid networkId, Guid deviceId, [FromQuery] string? role = null)
        {
            var success = await _svc.AttachDeviceAsync(networkId, deviceId, role);

            if (!success)
                return NotFound();

            return CreatedAtAction(
                nameof(GetDeviceById),
                new { networkId, deviceId },
                null
            );
        }


        [HttpDelete("{networkId:guid}/devices/{deviceId:guid}")]
        public async Task<IActionResult> DetachDevice(Guid networkId, Guid deviceId)
        {
            var success = await _svc.DetachDeviceAsync(networkId, deviceId);

            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}
