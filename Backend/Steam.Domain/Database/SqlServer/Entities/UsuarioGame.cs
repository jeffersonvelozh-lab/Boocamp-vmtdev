using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class UsuarioGame
{
    public int UsuarioId { get; set; }

    public int GameId { get; set; }

    public decimal? PurchasePrice { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
