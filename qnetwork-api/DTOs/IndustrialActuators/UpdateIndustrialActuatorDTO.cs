using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialActuators
{
    public class UpdateIndustrialActuatorDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IndustrialActuatorType? IndustrialActuatorType { get; set; }
        public IndustrialDeviceStatus? Status { get; set; }
    }
}
