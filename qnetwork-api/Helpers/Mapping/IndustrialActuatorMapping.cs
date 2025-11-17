using qnetwork_api.DTOs.IndustrialActuators;
using qnetwork_api.Models;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.Helpers.Mapping
{
    public static class IndustrialActuatorMapping
    {
        public static IndustrialDevice ToIndustrialDevice(CreateIndustrialActuatorDTO dto)
        {
            return new IndustrialDevice
            {
                Name = dto.Name,
                Description = dto.Description,
                IndustrialDeviceType = IndustrialDeviceType.IndustrialActuator,
                IndustrialActuatorType = dto.IndustrialActuatorType,
                Status = dto.Status
            };
        }

        public static void ApplyUpdate(IndustrialDevice device, UpdateIndustrialActuatorDTO dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name)) device.Name = dto.Name;
            if (dto.Description != null) device.Description = dto.Description;
            if (dto.IndustrialActuatorType.HasValue) device.IndustrialActuatorType = dto.IndustrialActuatorType;
            if (dto.Status.HasValue) device.Status = dto.Status.Value;
        }

        public static IndustrialActuatorResponseDTO ToResponse(IndustrialDevice device)
        {
            return new IndustrialActuatorResponseDTO
            {
                ID = device.ID,
                Name = device.Name,
                Description = device.Description,
                IndustrialActuatorType = device.IndustrialActuatorType
                    ?? throw new InvalidOperationException("IndustrialActuatorType is null."),
                Status = device.Status
            };
        }
    }
}