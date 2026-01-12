namespace MMAppApi.DTO
{
    public class CreateArtistRequestDto
    {
        public long ArtistId { get; set; }
        public string? Title {  get; set; }

        public string? Description { get; set; }
        public List<long>? InstrumentIds { get; set; }
        public List<long>? GenreIds { get; set; }
    }
}
