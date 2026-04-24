using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class UsuarioLibrerium
{
    public int UsuarioId { get; set; }

    public int LibreriaId { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Librerium Libreria { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
