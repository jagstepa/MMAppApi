using System.ComponentModel.DataAnnotations;

namespace MMAppApi.DTO
{
    public class GenreDto
    {
        public long GenreId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
