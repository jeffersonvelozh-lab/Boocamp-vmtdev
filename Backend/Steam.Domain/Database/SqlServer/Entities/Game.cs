using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Game
{
    public int GameId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Precio { get; set; }

    public DateOnly? FechaCompra { get; set; }

    public int DesarrolladorId { get; set; }

    public int? EditorId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Desarrolladore Desarrollador { get; set; } = null!;

    public virtual Editore? Editor { get; set; }

    public virtual ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();

    public virtual ICollection<Librerium> Libreria { get; set; } = new List<Librerium>();

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UsuarioGame> UsuarioGames { get; set; } = new List<UsuarioGame>();

    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
