using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialSensors
{
    public class UpdateIndustrialSensorDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IndustrialSensorType? IndustrialSensorType { get; set; }
        public IndustrialDeviceStatus? Status { get; set; }
    }
}
