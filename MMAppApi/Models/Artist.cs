using System;
using System.Collections.Generic;

namespace MMAppApi.Models;

public partial class Artist
{
    public long ArtistId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Instrument> Instruments { get; set; } = new List<Instrument>();
}
