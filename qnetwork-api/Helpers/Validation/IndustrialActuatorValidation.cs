using qnetwork_api.DTOs.IndustrialActuators;
using qnetwork_api.Models.Enums;
using System;

namespace qnetwork_api.Helpers.Validation
{
    public static class IndustrialActuatorValidation
    {
        public static bool ValidateCreate(CreateIndustrialActuatorDTO dto, out string? error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                error = "Actuator name is required.";
                return false;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                error = "Actuator name must be between 3 and 50 characters.";
                return false;
            }

            if (dto.Description is not null && dto.Description.Length > 200)
            {
                error = "Description cannot exceed 200 characters.";
                return false;
            }

            if (!Enum.IsDefined(typeof(IndustrialActuatorType), dto.IndustrialActuatorType))
            {
                error = "Invalid industrial actuator type.";
                return false;
            }

            if (dto.Status == IndustrialDeviceStatus.Online)
            {
                error = "A new actuator cannot be created with Online status. Set it to Offline or Maintenance.";
                return false;
            }

            return true;
        }
    }
}
