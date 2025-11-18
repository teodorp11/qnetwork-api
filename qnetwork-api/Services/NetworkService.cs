using Microsoft.EntityFrameworkCore;
using qnetwork_api.Data;
using qnetwork_api.DTOs.Networks;
using qnetwork_api.Models;

namespace qnetwork_api.Services
{
    public class NetworkService : INetworkService
    {
        private readonly QNetworkContext _context;
        public NetworkService(QNetworkContext context)
        {
            _context = context;
        }

        public async Task<NetworkResponseDTO> CreateAsync(CreateNetworkDTO dto)
        {
            var n = new Network
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Networks.Add(n);
            await _context.SaveChangesAsync();

            return new NetworkResponseDTO { Id = n.ID, Name = n.Name, Description = n.Description };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var n = await _context.Networks.FindAsync(id);
            if (n == null) return false;
            _context.Networks.Remove(n);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<NetworkResponseDTO>> GetAllAsync()
        {
            var list = await _context.Networks.ToListAsync();
            return list.Select(n => new NetworkResponseDTO { Id = n.ID, Name = n.Name, Description = n.Description });
        }

        public async Task<NetworkResponseDTO?> GetByIdAsync(Guid id)
        {
            var n = await _context.Networks.FirstOrDefaultAsync(x => x.ID == id);
            if (n == null) return null;
            return new NetworkResponseDTO { Id = n.ID, Name = n.Name, Description = n.Description };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateNetworkDTO dto)
        {
            var n = await _context.Networks.FindAsync(id);
            if (n == null) return false;
            if (!string.IsNullOrWhiteSpace(dto.Name)) n.Name = dto.Name;
            if (dto.Description != null) n.Description = dto.Description;
            _context.Networks.Update(n);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AttachDeviceAsync(Guid networkId, Guid deviceId, string? role = null)
        {
            var networkExists = await _context.Networks.AnyAsync(n => n.ID == networkId);
            var deviceExists = await _context.IndustrialDevices.AnyAsync(d => d.ID == deviceId);

            if (!networkExists || !deviceExists)
                return false;

            var existing = await _context.IndustrialDeviceNetworkMappings
                .FirstOrDefaultAsync(m => m.NetworkID == networkId &&
                                          m.IndustrialDeviceID == deviceId);

            if (existing != null)
            {
                // Update existing relationship
                existing.Role = role ?? existing.Role;
                // EF tracks it automatically; no need for Update()
            }
            else
            {
                // Create new mapping
                _context.IndustrialDeviceNetworkMappings.Add(new IndustrialDeviceNetworkMapping
                {
                    IndustrialDeviceID = deviceId,
                    NetworkID = networkId,
                    Role = role
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DetachDeviceAsync(Guid networkId, Guid deviceId)
        {
            var existing = await _context.IndustrialDeviceNetworkMappings
                .FirstOrDefaultAsync(m => m.NetworkID == networkId &&
                                          m.IndustrialDeviceID == deviceId);

            if (existing == null)
                return false;

            _context.IndustrialDeviceNetworkMappings.Remove(existing);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<NetworkResponseDTO>> GetAllDevicesAsync(Guid networkId)
        {
            var list = await _context.IndustrialDeviceNetworkMappings
                .Where(m => m.NetworkID == networkId)
                .Include(m => m.IndustrialDevice)
                .ToListAsync();

            if (!list.Any())
                return Enumerable.Empty<NetworkResponseDTO>();

            return list.Select(n => new NetworkResponseDTO
            {
                Id = n.ID,
                Name = n.IndustrialDevice?.Name ?? string.Empty,
                Description = n.IndustrialDevice?.Description ?? string.Empty,
            });
        }


        public async Task<NetworkResponseDTO?> GetDeviceByIdAsync(Guid networkId, Guid deviceId)
        {
            // 1️⃣ Check if network exists
            var networkExists = await _context.Networks.AnyAsync(n => n.ID == networkId);
            if (!networkExists)
                return null; // Controller will return a 404

            // 2️⃣ Check if device exists
            var deviceExists = await _context.IndustrialDevices.AnyAsync(d => d.ID == deviceId);
            if (!deviceExists)
                return null; // Controller will return a 404

            // 3️⃣ Check if mapping exists and include the device
            var mapping = await _context.IndustrialDeviceNetworkMappings
                .Include(m => m.IndustrialDevice)
                .FirstOrDefaultAsync(m => m.NetworkID == networkId && m.IndustrialDeviceID == deviceId);

            if (mapping == null)
                return null; // Controller will return a 404 with mapping-specific message

            // 4️⃣ Return DTO
            return new NetworkResponseDTO
            {
                Id = mapping.ID,
                Name = mapping.IndustrialDevice?.Name ?? string.Empty,
                Description = mapping.IndustrialDevice?.Description ?? string.Empty,
                Role = mapping.Role
            };
        }

        public async Task<bool> NetworkExistsAsync(Guid networkId)
        {
            return await _context.Networks.AnyAsync(n => n.ID == networkId);
        }

        public async Task<bool> DeviceExistsAsync(Guid deviceId)
        {
            return await _context.IndustrialDevices.AnyAsync(d => d.ID == deviceId);
        }



    }
}
