using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Friend
{
    public Guid Userid { get; set; }

    public Guid Friendid { get; set; }

    public string? Status { get; set; }

    public DateTime Createdat { get; set; }

    public virtual User FriendNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
