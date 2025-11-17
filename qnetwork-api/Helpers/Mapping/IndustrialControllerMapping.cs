using qnetwork_api.DTOs.IndustrialControllers;
using qnetwork_api.Models;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.Helpers.Mapping
{
    public static class IndustrialControllerMapping
    {
        public static IndustrialDevice ToIndustrialDevice(CreateIndustrialControllerDTO dto)
        {
            return new IndustrialDevice
            {
                Name = dto.Name,
                Description = dto.Description,
                IndustrialDeviceType = IndustrialDeviceType.IndustrialController,
                IndustrialControllerType = dto.IndustrialControllerType,
                Status = dto.Status
            };
        }

        public static void ApplyUpdate(IndustrialDevice device, UpdateIndustrialControllerDTO dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name)) device.Name = dto.Name;
            if (dto.Description != null) device.Description = dto.Description;
            if (dto.IndustrialControllerType.HasValue) device.IndustrialControllerType = dto.IndustrialControllerType;
            if (dto.Status.HasValue) device.Status = dto.Status.Value;
        }

        public static IndustrialControllerResponseDTO ToResponse(IndustrialDevice device)
        {
            return new IndustrialControllerResponseDTO
            {
                ID = device.ID,
                Name = device.Name,
                Description = device.Description,
                IndustrialControllerType = device.IndustrialControllerType
                    ?? throw new InvalidOperationException("IndustrialControllerType is null."),
                Status = device.Status
            };
        }
    }
}