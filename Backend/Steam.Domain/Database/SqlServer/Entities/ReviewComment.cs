using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class ReviewComment
{
    public int CommentId { get; set; }

    public int? ReviewId { get; set; }

    public int? UsuarioId { get; set; }

    public string Comentario { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Review? Review { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
