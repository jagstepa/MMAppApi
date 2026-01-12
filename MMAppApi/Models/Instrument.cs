using System;
using System.Collections.Generic;

namespace MMAppApi.Models;

public partial class Instrument
{
    public long InstrumentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
}
