using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialActuators
{
    public class IndustrialActuatorResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IndustrialActuatorType? IndustrialActuatorType { get; set; }
        public IndustrialDeviceStatus Status { get; set; }
    }
}
