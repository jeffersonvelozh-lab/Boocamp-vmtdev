using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Steam.Domain.Database.SqlServer.Entities;

namespace Steam.Domain.Database.SqlServer.Context;

public partial class SteamcloneBdContext : DbContext
{
    public SteamcloneBdContext()
    {
    }

    public SteamcloneBdContext(DbContextOptions<SteamcloneBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Desarrolladore> Desarrolladores { get; set; }

    public virtual DbSet<Editore> Editores { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameSession> GameSessions { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Librerium> Libreria { get; set; }

    public virtual DbSet<Oferta> Ofertas { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioGame> UsuarioGames { get; set; }

    public virtual DbSet<UsuarioLibrerium> UsuarioLibreria { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;User=sa;Password=Admin1234@;DataBase=SteamcloneBD;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Desarrolladore>(entity =>
        {
            entity.HasKey(e => e.DesarrolladorId).HasName("PK__Desarrol__965BA7B0B700C10F");

            entity.Property(e => e.DesarrolladorId).HasColumnName("DesarrolladorID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Editore>(entity =>
        {
            entity.HasKey(e => e.EditorId).HasName("PK__Editores__986DCA2AD70EACEF");

            entity.Property(e => e.EditorId).HasColumnName("EditorID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => new { e.UsuarioId, e.FriendId }).HasName("PK__Friends__011111CE736505B5");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.FriendId).HasColumnName("FriendID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");

            entity.HasOne(d => d.FriendNavigation).WithMany(p => p.FriendFriendNavigations)
                .HasForeignKey(d => d.FriendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__FriendI__73BA3083");

            entity.HasOne(d => d.Usuario).WithMany(p => p.FriendUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__Usuario__72C60C4A");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__2AB897DDAECF54B0");

            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.DesarrolladorId).HasColumnName("DesarrolladorID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EditorId).HasColumnName("EditorID");
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Desarrollador).WithMany(p => p.Games)
                .HasForeignKey(d => d.DesarrolladorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Games__created_a__5535A963");

            entity.HasOne(d => d.Editor).WithMany(p => p.Games)
                .HasForeignKey(d => d.EditorId)
                .HasConstraintName("FK__Games__EditorID__5629CD9C");

            entity.HasMany(d => d.Generos).WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GenerosGame",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenerosGa__Gener__6477ECF3"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenerosGa__GameI__6383C8BA"),
                    j =>
                    {
                        j.HasKey("GameId", "GeneroId").HasName("PK__GenerosG__B02147FB73FFA89E");
                        j.ToTable("GenerosGames");
                        j.IndexerProperty<int>("GameId").HasColumnName("GameID");
                        j.IndexerProperty<int>("GeneroId").HasColumnName("GeneroID");
                    });
        });

        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Game_Ses__C9F49270BFB75C39");

            entity.ToTable("Game_Sessions");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Game).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Game_Sess__GameI__5EBF139D");

            entity.HasOne(d => d.Usuario).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Game_Sess__Usuar__5DCAEF64");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PK__Generos__A99D0268F5E94EEB");

            entity.Property(e => e.GeneroId).HasColumnName("GeneroID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Librerium>(entity =>
        {
            entity.HasKey(e => e.LibreriaId).HasName("PK__Libreria__D1A86F876D2853FC");

            entity.Property(e => e.LibreriaId).HasColumnName("LibreriaID");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Game).WithMany(p => p.Libreria)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Libreria__GameID__6A30C649");
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.OfertaId).HasName("PK__Ofertas__F2629429D8597A39");

            entity.Property(e => e.Desccuento).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Game).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Ofertas__GameId__2180FB33");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Reviewid).HasName("PK__Reviews__74BB75E66CB74F57");

            entity.HasIndex(e => new { e.UsuarioId, e.GameId }, "UQ_Usuario_Game").IsUnique();

            entity.Property(e => e.Comentario).HasMaxLength(255);
            entity.Property(e => e.Createdat).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Game).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Reviews__GameId__0B91BA14");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Reviews__Usuario__0A9D95DB");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Review_C__C3B4DFCA09CA3D06");

            entity.ToTable("Review_Comments");

            entity.Property(e => e.Comentario).HasMaxLength(255);
            entity.Property(e => e.Createdat).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("FK__Review_Co__Revie__0F624AF8");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Review_Co__Usuar__10566F31");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798225B852F");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105349B5BA4CE").IsUnique();

            entity.HasIndex(e => e.UusuarioNombre, "UQ__Usuarios__F43DD1477DCCA108").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("offline");
            entity.Property(e => e.Pass).HasMaxLength(255);
            entity.Property(e => e.UusuarioNombre).HasMaxLength(50);

            entity.HasMany(d => d.Games).WithMany(p => p.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "Wishlist",
                    r => r.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Wishlist__GameId__14270015"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Wishlist__Usuari__1332DBDC"),
                    j =>
                    {
                        j.HasKey("UsuarioId", "GameId").HasName("PK__Wishlist__E9966EC7BA9D4754");
                        j.ToTable("Wishlist");
                    });
        });

        modelBuilder.Entity<UsuarioGame>(entity =>
        {
            entity.HasKey(e => new { e.UsuarioId, e.GameId }).HasName("PK__UsuarioG__E9966EE540B6BA70");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("purchase_date");
            entity.Property(e => e.PurchasePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("purchase_price");

            entity.HasOne(d => d.Game).WithMany(p => p.UsuarioGames)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioGa__GameI__5AEE82B9");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioGames)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioGa__Usuar__59FA5E80");
        });

        modelBuilder.Entity<UsuarioLibrerium>(entity =>
        {
            entity.HasKey(e => new { e.UsuarioId, e.LibreriaId }).HasName("PK__UsuarioL__46276160FA94D438");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.LibreriaId).HasColumnName("LibreriaID");
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Libreria).WithMany(p => p.UsuarioLibreria)
                .HasForeignKey(d => d.LibreriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioLi__Libre__6EF57B66");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioLibreria)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioLi__Usuar__6E01572D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
