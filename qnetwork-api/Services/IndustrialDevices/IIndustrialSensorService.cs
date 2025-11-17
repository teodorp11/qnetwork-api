using qnetwork_api.DTOs.IndustrialSensors;

namespace qnetwork_api.Services.IndustrialDevices
{
    public interface IIndustrialSensorService
    {
        Task<IEnumerable<IndustrialSensorResponseDTO>> GetAllAsync();
        Task<IndustrialSensorResponseDTO?> GetByIdAsync(Guid id);
        Task<IndustrialSensorResponseDTO> CreateAsync(CreateIndustrialSensorDTO dto);
        Task<bool> UpdateAsync(Guid id, UpdateIndustrialSensorDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
