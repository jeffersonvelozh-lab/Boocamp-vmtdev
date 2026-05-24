using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class UserRole
{
    public Guid Userid { get; set; }

    public int Roleid { get; set; }

    public DateTime? Deleteat { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
