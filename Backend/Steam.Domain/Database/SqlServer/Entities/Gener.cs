using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Gener
{
    public Guid Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
