using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Genero
{
    public int GeneroId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
