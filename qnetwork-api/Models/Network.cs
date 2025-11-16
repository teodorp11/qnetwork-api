using System.ComponentModel.DataAnnotations;

namespace qnetwork_api.Models
{
    public class Network
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public ICollection<IndustrialDeviceNetworkMapping> IndustrialDeviceMappings { get; set; } = new List<IndustrialDeviceNetworkMapping>();
    }
}