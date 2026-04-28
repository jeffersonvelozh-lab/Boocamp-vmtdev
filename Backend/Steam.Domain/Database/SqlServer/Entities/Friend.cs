using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Friend
{
    public int UserId { get; set; }

    public int FriendId { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User FriendNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
