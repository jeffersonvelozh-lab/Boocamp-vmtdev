using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class GameSession
{
    public Guid Id { get; set; }

    public Guid Usuarioid { get; set; }

    public Guid Gameid { get; set; }

    public DateTime? Starttime { get; set; }

    public DateTime? Endtime { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User Usuario { get; set; } = null!;
}
