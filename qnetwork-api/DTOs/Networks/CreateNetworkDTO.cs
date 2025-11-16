using System.ComponentModel.DataAnnotations;

namespace qnetwork_api.DTOs.Networks
{
    public class CreateNetworkDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
