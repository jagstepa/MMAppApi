using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMAppApi.DTO;
using MMAppApi.Helpers;
using MMAppApi.Interfaces;
using MMAppApi.Models;

namespace MMAppApi.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MmappContext _context;
        public GenreRepository(MmappContext context)
        {
            _context = context;
        }

        public GenreDto Create(GenreDto genreDto)
        {
            Genre genre = GenreHelper.FromDto(genreDto);

            _context.Genres.Add(genre);
            _context.SaveChanges();

            GenreDto newGenreDto = GenreHelper.ToDto(genre);
            return newGenreDto;
        }
        public ActionResult<IEnumerable<GenreDto>> GetAll()
        {
            var genreList = _context.Genres.Select(g => new GenreDto
            {
                GenreId = g.GenreId,
                Name = g.Name,
                Description = g.Description
            }).ToList();

            return genreList;
        }

        public Genre? GetById(long id)
        {
            var genre = _context.Genres.Find(id);
            return genre;
        }

        public void Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
        }
        public Genre? Delete(long id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
            {
                return null;
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return genre;
        }
    }
}
