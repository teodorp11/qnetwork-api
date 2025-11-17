using qnetwork_api.DTOs.IndustrialControllers;
using qnetwork_api.Models.Enums;
using System;

namespace qnetwork_api.Helpers.Validation
{
    public static class IndustrialControllerValidation
    {
        public static bool ValidateCreate(CreateIndustrialControllerDTO dto, out string? error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                error = "Controller name is required.";
                return false;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                error = "Controller name must be between 3 and 50 characters.";
                return false;
            }

            if (dto.Description is not null && dto.Description.Length > 200)
            {
                error = "Description cannot exceed 200 characters.";
                return false;
            }

            if (!Enum.IsDefined(typeof(IndustrialControllerType), dto.IndustrialControllerType))
            {
                error = "Invalid industrial controller type.";
                return false;
            }

            if (dto.Status == IndustrialDeviceStatus.Online)
            {
                error = "A new controller cannot be created with Online status. Set it to Offline or Maintenance.";
                return false;
            }

            return true;
        }
    }
}
