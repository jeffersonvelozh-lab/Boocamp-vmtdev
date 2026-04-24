using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Friend
{
    public int UsuarioId { get; set; }

    public int FriendId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Usuario FriendNavigation { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
