using Microsoft.AspNetCore.Mvc;
using MMAppApi.DTO;
using MMAppApi.Models;

namespace MMAppApi.Interfaces
{
    public interface IGenreRepository
    {
        ActionResult<IEnumerable<GenreDto>> GetAll();
        GenreDto Create(GenreDto genre);
        Genre? GetById(long id);
        void Update(Genre genre);
        Genre? Delete(long id);
    }
}
