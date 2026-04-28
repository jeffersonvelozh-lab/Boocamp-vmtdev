using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Steam.Domain.Database.SqlServer.Entities;

namespace Steam.Domain.Database.SqlServer.Context;

public partial class SteamCloneContext : DbContext
{
    public SteamCloneContext()
    {
    }

    public SteamCloneContext(DbContextOptions<SteamCloneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Editore> Editores { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameSession> GameSessions { get; set; }

    public virtual DbSet<Gener> Geners { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserGame> UserGames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;User=sa;Password=Admin1234@;DataBase=SteamClone;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.AchievementId).HasName("PK__Achievem__3C492E83A0389CC3");

            entity.Property(e => e.AchievementId).HasColumnName("achievement_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Game).WithMany(p => p.Achievements)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Achieveme__game___59FA5E80");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.DeveloperId).HasName("PK__Develope__F4FA438008FEE8F1");

            entity.Property(e => e.DeveloperId).HasColumnName("developer_id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasColumnName("website");
        });

        modelBuilder.Entity<Editore>(entity =>
        {
            entity.HasKey(e => e.EditorId).HasName("PK__Editores__582CA82C4F2C2231");

            entity.Property(e => e.EditorId).HasColumnName("editorID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FriendId }).HasName("PK__Friends__FA44291AA90B0799");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.FriendId).HasColumnName("friend_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.FriendNavigation).WithMany(p => p.FriendFriendNavigations)
                .HasForeignKey(d => d.FriendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__friend___571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.FriendUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__user_id__5629CD9C");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__FFE11FCF74E463C0");

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DeveloperId).HasColumnName("developer_id");
            entity.Property(e => e.Price)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("release_date");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Developer).WithMany(p => p.Games)
                .HasForeignKey(d => d.DeveloperId)
                .HasConstraintName("FK__Games__developer__4316F928");

            entity.HasMany(d => d.Geners).WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GenerGame",
                    r => r.HasOne<Gener>().WithMany()
                        .HasForeignKey("GenerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenerGame__gener__4CA06362"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenerGame__game___4BAC3F29"),
                    j =>
                    {
                        j.HasKey("GameId", "GenerId").HasName("PK__GenerGam__CC043CF522295BE7");
                        j.ToTable("GenerGame");
                        j.IndexerProperty<int>("GameId").HasColumnName("game_id");
                        j.IndexerProperty<int>("GenerId").HasColumnName("gener_id");
                    });
        });

        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Game_Ses__23DB12CBDFCCA34D");

            entity.ToTable("Game_Sessions");

            entity.Property(e => e.SessionId).HasColumnName("sessionID");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.GameId).HasColumnName("gameID");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioID");

            entity.HasOne(d => d.Game).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Game_Sess__gameI__46E78A0C");

            entity.HasOne(d => d.Usuario).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Game_Sess__usuar__45F365D3");
        });

        modelBuilder.Entity<Gener>(entity =>
        {
            entity.HasKey(e => e.GenerId).HasName("PK__Geners__3E5233A2BB56092E");

            entity.Property(e => e.GenerId).HasColumnName("gener_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.OfferId).HasName("PK__Offers__03D37AC252C7F98F");

            entity.Property(e => e.OfferId).HasColumnName("offer_id");
            entity.Property(e => e.DiscountPct)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_pct");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Game).WithMany(p => p.Offers)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Offers__game_id__70DDC3D8");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__60883D9072DCA24E");

            entity.HasIndex(e => new { e.UserId, e.GameId }, "UQ_User_Game").IsUnique();

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Game).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Reviews__game_id__656C112C");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__user_id__6477ECF3");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Review_C__E7957687DA06389E");

            entity.ToTable("Review_Comments");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("FK__Review_Co__revie__693CA210");

            entity.HasOne(d => d.User).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Review_Co__user___6A30C649");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FEA4FBF30");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164C526DFD4").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5720FC0E869").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LastLogin)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("last_login");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasMany(d => d.Games).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Wishlist",
                    r => r.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Wishlist__game_i__6E01572D"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Wishlist__user_i__6D0D32F4"),
                    j =>
                    {
                        j.HasKey("UserId", "GameId").HasName("PK__Wishlist__564026F3AC95D030");
                        j.ToTable("Wishlist");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("GameId").HasColumnName("game_id");
                    });
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AchievementId }).HasName("PK__User_Ach__9A7AA5E76B87568B");

            entity.ToTable("User_Achievements");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AchievementId).HasColumnName("achievement_id");
            entity.Property(e => e.UnlockedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("unlocked_at");

            entity.HasOne(d => d.Achievement).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.AchievementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Achi__achie__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Achi__user___5DCAEF64");
        });

        modelBuilder.Entity<UserGame>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GameId }).HasName("PK__User_Gam__564026F3161E783D");

            entity.ToTable("User_Games");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.PlaytimeHours)
                .HasDefaultValue(0)
                .HasColumnName("playtime_hours");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("purchase_date");

            entity.HasOne(d => d.Game).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Game__game___52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Game__user___5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
