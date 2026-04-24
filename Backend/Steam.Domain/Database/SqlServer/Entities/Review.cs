using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Review
{
    public int Reviewid { get; set; }

    public int? UsuarioId { get; set; }

    public int? GameId { get; set; }

    public string Comentario { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual Game? Game { get; set; }

    public virtual ICollection<ReviewComment> ReviewComments { get; set; } = new List<ReviewComment>();

    public virtual Usuario? Usuario { get; set; }
}
