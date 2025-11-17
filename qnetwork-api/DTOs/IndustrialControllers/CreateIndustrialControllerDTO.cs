using System.ComponentModel.DataAnnotations;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialControllers
{
    public class CreateIndustrialControllerDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
        
        [Required]
        public IndustrialControllerType IndustrialControllerType { get; set; }
        
        public IndustrialDeviceStatus Status { get; set; } = IndustrialDeviceStatus.Offline;
    }
}
