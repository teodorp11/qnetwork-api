using Microsoft.EntityFrameworkCore;
using qnetwork_api.Data;
using qnetwork_api.DTOs.IndustrialControllers;
using qnetwork_api.Helpers.Mapping;
using qnetwork_api.Helpers.Validation;
using qnetwork_api.Models;
using qnetwork_api.Services.IndustrialDevices;

namespace qnetwork_api.Services.Devices
{
    public class IndustrialControllerService : IIndustrialControllerService
    {
        private readonly QNetworkContext _context;

        public IndustrialControllerService(QNetworkContext context)
        {
            _context = context;
        }

        public async Task<IndustrialControllerResponseDTO> CreateAsync(CreateIndustrialControllerDTO dto)
        {
            if (!IndustrialControllerValidation.ValidateCreate(dto, out var err))
                throw new ArgumentException(err);

            var device = IndustrialControllerMapping.ToIndustrialDevice(dto);
            _context.IndustrialDevices.Add(device);
            await _context.SaveChangesAsync();
            return IndustrialControllerMapping.ToResponse(device);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialController);
            if (device == null) return false;
            _context.IndustrialDevices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<IndustrialControllerResponseDTO>> GetAllAsync()
        {
            var list = await _context.IndustrialDevices.Where(d => d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialController).ToListAsync();
            return list.Select(IndustrialControllerMapping.ToResponse);
        }

        public async Task<IndustrialControllerResponseDTO?> GetByIdAsync(Guid id)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialController);
            if (device == null) return null;
            return IndustrialControllerMapping.ToResponse(device);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateIndustrialControllerDTO dto)
        {
            var device = await _context.IndustrialDevices.FirstOrDefaultAsync(d => d.ID == id && d.IndustrialDeviceType == Models.Enums.IndustrialDeviceType.IndustrialController);
            if (device == null) return false;
            IndustrialControllerMapping.ApplyUpdate(device, dto);
            _context.IndustrialDevices.Update(device);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
