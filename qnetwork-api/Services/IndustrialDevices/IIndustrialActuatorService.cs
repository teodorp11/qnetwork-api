using qnetwork_api.DTOs.IndustrialActuators;

namespace qnetwork_api.Services.IndustrialDevices
{
    public interface IIndustrialActuatorService
    {
        Task<IEnumerable<IndustrialActuatorResponseDTO>> GetAllAsync();
        Task<IndustrialActuatorResponseDTO?> GetByIdAsync(Guid id);
        Task<IndustrialActuatorResponseDTO> CreateAsync(CreateIndustrialActuatorDTO dto);
        Task<bool> UpdateAsync(Guid id, UpdateIndustrialActuatorDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
