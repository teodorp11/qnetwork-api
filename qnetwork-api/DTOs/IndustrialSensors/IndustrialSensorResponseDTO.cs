using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialSensors
{
    public class IndustrialSensorResponseDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IndustrialSensorType IndustrialSensorType { get; set; }
        public IndustrialDeviceStatus Status { get; set; }
    }
}
