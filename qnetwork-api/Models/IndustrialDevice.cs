using System.ComponentModel.DataAnnotations;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.Models
{
    public class IndustrialDevice
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(120)]
        public required string Name { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
        public IndustrialDeviceType IndustrialDeviceType { get; set; }

        public IndustrialDeviceStatus Status { get; set; }

        // Type-specific enums
        public IndustrialSensorType? IndustrialSensorType { get; set; }
        public IndustrialActuatorType? IndustrialActuatorType { get; set; }
        public IndustrialControllerType? IndustrialControllerType  { get; set; }

        // Network mappings
        public ICollection<IndustrialDeviceNetworkMapping> NetworkMappings { get; set; } = new List<IndustrialDeviceNetworkMapping>();


        // Metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastSeenAt { get; set; }

    }
}
