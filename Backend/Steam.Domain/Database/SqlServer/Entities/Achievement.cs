using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Achievement
{
    public int AchievementId { get; set; }

    public int? GameId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public virtual Game? Game { get; set; }

    public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
