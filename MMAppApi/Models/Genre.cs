using System;
using System.Collections.Generic;

namespace MMAppApi.Models;

public partial class Genre
{
    public long GenreId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
}
