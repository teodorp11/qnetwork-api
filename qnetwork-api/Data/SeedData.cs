using qnetwork_api.Models;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.Data
{
    public static class SeedData
    {
        public static void Initialize(QNetworkContext context)
        {
            if (context.IndustrialDevices.Any() || context.Networks.Any())
            {
                return;
            }

            var n1 = new Network { Name = "Factory Floor A", Description = "Main production floor network" };
            var n2 = new Network { Name = "Factory Floor B"};
            var n3 = new Network { Name = "Factory Floor C"};
            var n4 = new Network { Name = "Testing Lab", Description = "Lab network for QA" };

            var d1 = new IndustrialDevice
            {
                Name = "TempSensor-01",
                Description = "Temperature sensor near press",
                IndustrialDeviceType = IndustrialDeviceType.IndustrialSensor,
                IndustrialSensorType = IndustrialSensorType.TemperatureSensor,
                Status = IndustrialDeviceStatus.Offline
            };

            var d2 = new IndustrialDevice
            {
                Name = "ValveAct-01",
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
                Status = IndustrialDeviceStatus.Offline
            };

            var d4 = new IndustrialDevice
            {
                Name = "TempSensor-02",
                IndustrialDeviceType = IndustrialDeviceType.IndustrialSensor,
                IndustrialSensorType = IndustrialSensorType.TemperatureSensor,
                Status = IndustrialDeviceStatus.Offline
            };

            var d5 = new IndustrialDevice
            {
                Name = "ValveAct-02",
                IndustrialDeviceType = IndustrialDeviceType.IndustrialActuator,
                IndustrialActuatorType = IndustrialActuatorType.ValveActuator,
                Status = IndustrialDeviceStatus.Maintenance
            };

            var d6 = new IndustrialDevice
            {
                Name = "PLC-Gateway-02",
                IndustrialDeviceType = IndustrialDeviceType.IndustrialController,
                IndustrialControllerType = IndustrialControllerType.PLC,
                Status = IndustrialDeviceStatus.Offline
            };

            context.Networks.AddRange(n1, n2, n3, n4);
            context.IndustrialDevices.AddRange(d1, d2, d3, d4, d5, d6);
            context.SaveChanges();

            context.IndustrialDeviceNetworkMappings.AddRange(
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d1.ID, NetworkID = n1.ID},
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d2.ID, NetworkID = n1.ID},
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d3.ID, NetworkID = n1.ID},
                new IndustrialDeviceNetworkMapping { IndustrialDeviceID = d1.ID, NetworkID = n2.ID}
            );

            context.SaveChanges();
        }
    }
}
