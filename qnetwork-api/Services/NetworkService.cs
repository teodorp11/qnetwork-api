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
            var network = await _context.Networks.FindAsync(networkId);
            var device = await _context.IndustrialDevices.FindAsync(deviceId);
            if (network == null || device == null) return false;

            var existing = await _context.IndustrialDeviceNetworkMappings.FirstOrDefaultAsync(m => m.NetworkID == networkId && m.IndustrialDeviceID == deviceId);
            if (existing != null)
            {
                existing.Role = role ?? existing.Role;
                _context.IndustrialDeviceNetworkMappings.Update(existing);
            }
            else
            {
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
            var existing = await _context.IndustrialDeviceNetworkMappings.FirstOrDefaultAsync(m => m.NetworkID == networkId && m.IndustrialDeviceID == deviceId);
            if (existing == null) return false;
            _context.IndustrialDeviceNetworkMappings.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
