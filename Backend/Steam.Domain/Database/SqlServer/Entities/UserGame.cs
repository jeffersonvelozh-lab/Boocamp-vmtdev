using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class UserGame
{
    public Guid Userid { get; set; }

    public Guid Gameid { get; set; }

    public DateTime Purchasedate { get; set; }

    public int? Playtimehours { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
