using qnetwork_api.DTOs.IndustrialSensors;
using qnetwork_api.Models;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.Helpers.Mapping
{
    public static class IndustrialSensorMapping
    {
        public static IndustrialDevice ToIndustrialDevice(CreateIndustrialSensorDTO dto)
        {
            return new IndustrialDevice
            {
                Name = dto.Name,
                Description = dto.Description,
                IndustrialDeviceType = IndustrialDeviceType.IndustrialSensor,
                IndustrialSensorType = dto.IndustrialSensorType,
                Status = dto.Status
            };
        }

        public static void ApplyUpdate(IndustrialDevice device, UpdateIndustrialSensorDTO dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name)) device.Name = dto.Name;
            if (dto.Description != null) device.Description = dto.Description;
            if (dto.IndustrialSensorType.HasValue) device.IndustrialSensorType = dto.IndustrialSensorType;
            if (dto.Status.HasValue) device.Status = dto.Status.Value;
        }

        public static IndustrialSensorResponseDTO ToResponse(IndustrialDevice device)
        {
            return new IndustrialSensorResponseDTO
            {
                ID = device.ID,
                Name = device.Name,
                Description = device.Description,
                IndustrialSensorType = device.IndustrialSensorType
                    ?? throw new InvalidOperationException("IndustrialSensorType is null."),
                Status = device.Status
            };
        }
    }
}