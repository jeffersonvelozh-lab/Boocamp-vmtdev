using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Offer
{
    public int OfferId { get; set; }

    public int? GameId { get; set; }

    public decimal? DiscountPct { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Game? Game { get; set; }
}
