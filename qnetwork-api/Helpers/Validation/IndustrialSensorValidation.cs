using qnetwork_api.DTOs.IndustrialSensors;
using qnetwork_api.Models.Enums;

namespace qnetwork_api.Helpers.Validation
{
    public static class IndustrialSensorValidation
    {
        public static bool ValidateCreate(CreateIndustrialSensorDTO dto, out string? error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                error = "Sensor name is required.";
                return false;
            }

            if (dto.Name.Length < 3 || dto.Name.Length > 50)
            {
                error = "Sensor name must be between 3 and 50 characters.";
                return false;
            }

            if (dto.Description is not null && dto.Description.Length > 200)
            {
                error = "Description cannot exceed 200 characters.";
                return false;
            }

            if (!Enum.IsDefined(typeof(IndustrialSensorType), dto.IndustrialSensorType))
            {
                error = "Invalid industrial sensor type.";
                return false;
            }

            if (dto.Status == IndustrialDeviceStatus.Online)
            {
                error = "A new sensor cannot be created with Online status. Set it to Offline or Maintenance.";
                return false;
            }

            return true;
        }
    }
}
