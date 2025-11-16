using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qnetwork_api.Models
{
    public class IndustrialDeviceNetworkMapping
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        
        public Guid IndustrialDeviceID { get; set; }
        
        [ForeignKey(nameof(IndustrialDeviceID))]
        public IndustrialDevice? IndustrialDevice { get; set; }

        [Required]
        public Guid NetworkID { get; set; }

        [ForeignKey(nameof(NetworkID))]
        public Network? Network { get; set; }

        public string? Role { get; set; }
    }
}
