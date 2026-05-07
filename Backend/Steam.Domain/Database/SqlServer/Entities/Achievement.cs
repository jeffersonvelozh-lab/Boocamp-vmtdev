using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Achievement
{
    public Guid Id { get; set; }

    public Guid Gameid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
