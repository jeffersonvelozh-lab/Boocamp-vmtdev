using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class UserAchievement
{
    public Guid Userid { get; set; }

    public Guid Achievementid { get; set; }

    public DateTime Unlockedat { get; set; }

    public virtual Achievement Achievement { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
