using qnetwork_api.Models.Enums;

namespace qnetwork_api.Models
{
    public class IndustrialDevice
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public IndustrialDeviceType Type { get; set; }

        public IndustrialSensorType? IndustrialSensorType { get; set; }
        public IndustrialActuatorType? IndustrialActuatorType { get; set; }
        public IndustrialControllerType? IndustrialControllerType  { get; set; }

        public string Location { get; set; }
        public IndustrialDeviceStatus Status { get; set; }
        public DateTime LastMaintenanceDate { get; set; } = DateTime.UtcNow;

    }
}
