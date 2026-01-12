namespace MMAppApi.DTO
{
    public class ArtistIncludeDto
    {
        public long ArtistId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<GenreDto>? Genres { get; set; }  
        public List<InstrumentDto>? Instruments { get; set; }
    }
}
