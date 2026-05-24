using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Genre
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? Deleteat { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
