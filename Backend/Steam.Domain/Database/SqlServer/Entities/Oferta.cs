using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Oferta
{
    public int OfertaId { get; set; }

    public int? GameId { get; set; }

    public decimal? Desccuento { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public virtual Game? Game { get; set; }
}
