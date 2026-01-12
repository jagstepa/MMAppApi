using MMAppApi.DTO;
using MMAppApi.Models;

namespace MMAppApi.Helpers
{
    public static class GenreHelper
    {
        public static GenreDto ToDto(Genre genre)
        {
            return new GenreDto
            {
                GenreId = genre.GenreId,
                Name = genre.Name,
                Description = genre.Description
            };
        }

        public static Genre FromDto(GenreDto dto)
        {
            return new Genre
            {
                GenreId = dto.GenreId,
                Name = dto.Name,
                Description = dto.Description
            };
        }
    }
}
