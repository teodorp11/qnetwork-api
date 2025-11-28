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
    }
}
