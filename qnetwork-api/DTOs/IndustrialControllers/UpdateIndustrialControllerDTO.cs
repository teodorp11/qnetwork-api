using qnetwork_api.Models.Enums;

namespace qnetwork_api.DTOs.IndustrialControllers
{
    public class UpdateIndustrialControllerDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IndustrialControllerType? IndustrialControllerType { get; set; }
        public IndustrialDeviceStatus? Status { get; set; }
    }
}
