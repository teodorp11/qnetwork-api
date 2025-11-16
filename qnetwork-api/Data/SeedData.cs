using qnetwork_api.Models;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.Data
{
    public static class SeedData
    {
        public static void Initialize(QNetworkContext context)
        {
            if (context.IndustrialDevices.Any() || context.Networks.Any())
                return;

            var n1 = new Network { Name = "Factory Floor A", Description = "Main production floor network" };
            var n2 = new Network { Name = "Testing Lab", Description = "Lab network for QA" };

            var d1 = new IndustrialDevice
            {
                Name = "TempSensor-1",
                Description = "Temperature sensor near press #2",
                IndustrialDeviceType = IndustrialDeviceType.IndustrialSensor,
                IndustrialSensorType = IndustrialSensorType.TemperatureSensor,
                Status = IndustrialDeviceStatus.Online,
                LastSeenAt = DateTime.UtcNow
            };

            var d2 = new IndustrialDevice
            {
                Name = "ValveAct-03",
                Description = "Control valve actuator for coolant loop",
                IndustrialDeviceType = IndustrialDeviceType.IndustrialActuator,
                IndustrialActuatorType = IndustrialActuatorType.ValveActuator,
                Status = IndustrialDeviceStatus.Maintenance
            };

            var d3 = new IndustrialDevice
            {
                Name = "PLC-Gateway-01",
                IndustrialDeviceType = IndustrialDeviceType.IndustrialController,
                IndustrialControllerType = IndustrialControllerType.PLC,
                Status = IndustrialDeviceStatus.Online
            };

            context.Networks.AddRange(n1, n2);
            context.IndustrialDevices.AddRange(d1, d2, d3);
            context.SaveChanges();

            context.IndustrialDeviceNetworkMappings.AddRange(
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d1.ID, NetworkID = n1.ID, Role = "sensor-node" },
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d2.ID, NetworkID = n1.ID, Role = "actuator-node" },
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d3.ID, NetworkID = n1.ID, Role = "gateway" },
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d1.ID, NetworkID = n2.ID, Role = "diag" }
            );

            context.SaveChanges();
        }
    }
}
