using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Review
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid Gameid { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime Createdat { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual ICollection<ReviewComment> ReviewComments { get; set; } = new List<ReviewComment>();

    public virtual User User { get; set; } = null!;
}
