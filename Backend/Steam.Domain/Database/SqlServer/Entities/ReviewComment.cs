using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class ReviewComment
{
    public int Id { get; set; }

    public Guid Reviewid { get; set; }

    public Guid Userid { get; set; }

    public string? Comment { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Review Review { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
