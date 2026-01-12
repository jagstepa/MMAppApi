using System;
using System.Collections.Generic;

namespace MMAppApi.Models;

public partial class Occupation
{
    public long OccupationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
