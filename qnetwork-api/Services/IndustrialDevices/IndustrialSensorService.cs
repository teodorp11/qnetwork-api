using Microsoft.EntityFrameworkCore;
using qnetwork_api.Data;
using qnetwork_api.DTOs.IndustrialSensors;
using qnetwork_api.Helpers;
using qnetwork_api.Helpers.Mapping;
using qnetwork_api.Helpers.Validation;
using qnetwork_api.Models;
using qnetwork_api.Services.IndustrialDevices;

namespace qnetwork_api.Services.IndustrialDevices
{
    public class IndustrialSensorService : IIndustrialSensorService
    {
        private readonly QNetworkContext _context;

        public IndustrialSensorService(QNetworkContext context)
        {
            _context = context;
        }

        public async Task<IndustrialSensorResponseDTO> CreateAsync(CreateIndustrialSensorDTO dto)
        {
            if (!IndustrialSensorValidation.ValidateCreate(dto, out var err))
                throw new ArgumentException(err);

            var device = IndustrialSensorMapping.ToIndustrialDevice(dto);
            _context.IndustrialDevices.Add(device);
            await _context.SaveChangesAsync();
            return IndustrialSensorMapping.ToResponse(device);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialSensor);
            if (device == null) return false;
            _context.IndustrialDevices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<IndustrialSensorResponseDTO>> GetAllAsync()
        {
            var list = await _context.IndustrialDevices.Where(d => d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialSensor).ToListAsync();
            return list.Select(IndustrialSensorMapping.ToResponse);
        }

        public async Task<IndustrialSensorResponseDTO?> GetByIdAsync(Guid id)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialSensor);
            if (device == null) return null;
            return IndustrialSensorMapping.ToResponse(device);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateIndustrialSensorDTO dto)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialSensor);
            if (device == null) return false;
            IndustrialSensorMapping.ApplyUpdate(device, dto);
            _context.IndustrialDevices.Update(device);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
