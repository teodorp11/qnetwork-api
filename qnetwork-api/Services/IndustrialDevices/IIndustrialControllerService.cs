using qnetwork_api.DTOs.IndustrialControllers;

namespace qnetwork_api.Services.IndustrialDevices
{
    public interface IIndustrialControllerService
    {
        Task<IEnumerable<IndustrialControllerResponseDTO>> GetAllAsync();
        Task<IndustrialControllerResponseDTO?> GetByIdAsync(Guid id);
        Task<IndustrialControllerResponseDTO> CreateAsync(CreateIndustrialControllerDTO dto);
        Task<bool> UpdateAsync(Guid id, UpdateIndustrialControllerDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
