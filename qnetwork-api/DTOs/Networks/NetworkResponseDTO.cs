namespace qnetwork_api.DTOs.Networks
{
    public class NetworkResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
