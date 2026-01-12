using Microsoft.EntityFrameworkCore;
using MMAppApi.DTO;
using MMAppApi.Models;

namespace MMAppApi.Helpers
{
    public static class ArtistHelper
    {
        public static Artist AddArtist(MmappContext context, CreateArtistRequestDto request)
        {
            Artist artist = new Artist();

            if (request.Title != null)
            {
                artist.Name = request.Title;
            }

            artist.Description = request.Description;

            if (request.InstrumentIds != null)
            {
                foreach (var InstrumentId in request.InstrumentIds)
                {
                    var instrument = context.Instruments.Find(InstrumentId);

                    if (instrument != null)
                    {
                        artist.Instruments.Add(instrument);
                    }
                }
            }

            if (request.GenreIds != null)
            {
                foreach (var GenreId in request.GenreIds)
                {
                    var genre = context.Genres.Find(GenreId);

                    if (genre != null)
                    {
                        artist.Genres.Add(genre);
                    }
                }
            }

            return artist;
        }
    }
}
