using System;
using System.Collections.Generic;

namespace MMAppApi.Models;

public partial class Website
{
    public long WebsiteId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
