using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Wishlist
{
    public Guid Userid { get; set; }

    public Guid Gameid { get; set; }

    public DateTime Addedat { get; set; }

    public DateTime? Deleteat { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
