using System.Collections.Generic;

namespace MMAppApi.DTO
{
    public class ArtistDto
    {
        public long ArtistId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
