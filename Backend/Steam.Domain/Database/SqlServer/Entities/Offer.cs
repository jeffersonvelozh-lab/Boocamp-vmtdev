using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Offer
{
    public int Offerid { get; set; }

    public Guid Gameid { get; set; }

    public decimal? Discountpct { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public virtual Game Game { get; set; } = null!;
}
