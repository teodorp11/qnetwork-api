using System.ComponentModel.DataAnnotations;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialActuators
{
    public class CreateIndustrialActuatorDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
        
        [Required]
        public IndustrialActuatorType IndustrialActuatorType { get; set; }

        public IndustrialDeviceStatus Status { get; set; } = IndustrialDeviceStatus.Offline;
    }
}
