using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Developer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Country { get; set; }

    public string? Website { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
