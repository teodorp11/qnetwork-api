using Microsoft.EntityFrameworkCore;
using qnetwork_api.Data;
using qnetwork_api.DTOs.IndustrialActuators;
using qnetwork_api.Helpers;
using qnetwork_api.Helpers.Mapping;
using qnetwork_api.Helpers.Validation;
using qnetwork_api.Models;
using qnetwork_api.Services.Devices;

namespace qnetwork_api.Services.Devices
{
    public class IndustrialActuatorService : IIndustrialActuatorService
    {
        private readonly QNetworkContext _context;

        public IndustrialActuatorService(QNetworkContext context)
        {
            _context = context;
        }

        public async Task<IndustrialActuatorResponseDTO> CreateAsync(CreateIndustrialActuatorDTO dto)
        {
            if (!IndustrialActuatorValidation.ValidateCreate(dto, out var err))
                throw new ArgumentException(err);

            var device = IndustrialActuatorMapping.ToIndustrialDevice(dto);
            _context.IndustrialDevices.Add(device);
            await _context.SaveChangesAsync();
            return IndustrialActuatorMapping.ToResponse(device);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialActuator);
            if (device == null) return false;
            _context.IndustrialDevices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<IndustrialActuatorResponseDTO>> GetAllAsync()
        {
            var list = await _context.IndustrialDevices.Where(d => d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialActuator).ToListAsync();
            return list.Select(IndustrialActuatorMapping.ToResponse);
        }

        public async Task<IndustrialActuatorResponseDTO?> GetByIdAsync(Guid id)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialActuator);
            if (device == null) return null;
            return IndustrialActuatorMapping.ToResponse(device);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateIndustrialActuatorDTO dto)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialActuator);
            if (device == null) return false;
            IndustrialActuatorMapping.ApplyUpdate(device, dto);
            _context.IndustrialDevices.Update(device);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
