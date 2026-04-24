using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Librerium
{
    public int LibreriaId { get; set; }

    public int? GameId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual Game? Game { get; set; }

    public virtual ICollection<UsuarioLibrerium> UsuarioLibreria { get; set; } = new List<UsuarioLibrerium>();
}
