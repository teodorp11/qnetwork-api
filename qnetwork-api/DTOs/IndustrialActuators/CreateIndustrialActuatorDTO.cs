using System.ComponentModel.DataAnnotations;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialActuators
{
    public class CreateIndustrialActuatorDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        [Required]
        public IndustrialActuatorType IndustriaActuatorType { get; set; }
        public IndustrialDeviceStatus Status { get; set; } = IndustrialDeviceStatus.Offline;
    }
}
