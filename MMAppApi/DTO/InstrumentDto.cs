namespace MMAppApi.DTO
{
    public class InstrumentDto
    {
        public long InstrumentId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
