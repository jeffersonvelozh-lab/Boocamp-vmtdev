using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class ReviewComment
{
    public int CommentId { get; set; }

    public int? ReviewId { get; set; }

    public int? UserId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Review? Review { get; set; }

    public virtual User? User { get; set; }
}
