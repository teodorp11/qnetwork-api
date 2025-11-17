using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialControllers
{
    public class IndustrialControllerResponseDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public IndustrialControllerType IndustrialControllerType { get; set; }
        public IndustrialDeviceStatus Status { get; set; }
    }
}
