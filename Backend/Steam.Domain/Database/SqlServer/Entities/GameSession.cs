using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class GameSession
{
    public int SessionId { get; set; }

    public int? UsuarioId { get; set; }

    public int? GameId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual Game? Game { get; set; }

    public virtual User? Usuario { get; set; }
}
