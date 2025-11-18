using qnetwork_api.DTOs.Networks;

namespace qnetwork_api.Services
{
    public interface INetworkService
    {
        Task<IEnumerable<NetworkResponseDTO>> GetAllAsync();
        Task<NetworkResponseDTO?> GetByIdAsync(Guid id);
        Task<NetworkResponseDTO> CreateAsync(CreateNetworkDTO dto);
        Task<bool> UpdateAsync(Guid id, UpdateNetworkDTO dto);
        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<NetworkResponseDTO>?> GetAllDevicesAsync(Guid networkId);
        Task<NetworkResponseDTO?> GetDeviceByIdAsync(Guid networkId, Guid deviceId);
        Task<bool> AttachDeviceAsync(Guid networkId, Guid deviceId, string? role = null);
        Task<bool> DetachDeviceAsync(Guid networkId, Guid deviceId);
        Task<bool> NetworkExistsAsync(Guid networkId);
        Task<bool> DeviceExistsAsync(Guid deviceId);
    }
}
