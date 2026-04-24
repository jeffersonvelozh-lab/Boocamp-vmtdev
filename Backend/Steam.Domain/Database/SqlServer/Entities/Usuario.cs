using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string UusuarioNombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string? Estado { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Friend> FriendFriendNavigations { get; set; } = new List<Friend>();

    public virtual ICollection<Friend> FriendUsuarios { get; set; } = new List<Friend>();

    public virtual ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();

    public virtual ICollection<ReviewComment> ReviewComments { get; set; } = new List<ReviewComment>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UsuarioGame> UsuarioGames { get; set; } = new List<UsuarioGame>();

    public virtual ICollection<UsuarioLibrerium> UsuarioLibreria { get; set; } = new List<UsuarioLibrerium>();

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
