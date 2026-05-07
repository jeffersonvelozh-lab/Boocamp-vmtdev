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
            entity.HasKey(e => e.Id).HasName("PK__Achievem__3213E83F8D8B2CE5");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Game).WithMany(p => p.Achievements)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Achieveme__gamei__72C60C4A");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Develope__3213E83F1252D70E");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__Editores__3213E83F3B78583C");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Friendid }).HasName("PK__Friends__2D047465C7ADA9AE");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Friendid).HasColumnName("friendid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.FriendNavigation).WithMany(p => p.FriendFriendNavigations)
                .HasForeignKey(d => d.Friendid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__friendi__6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.FriendUsers)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__userid__6E01572D");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3213E83FF2B8CD28");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Developerid).HasColumnName("developerid");
            entity.Property(e => e.Price)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Publisherid).HasColumnName("publisherid");
            entity.Property(e => e.Releasedate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("releasedate");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Developer).WithMany(p => p.Games)
                .HasForeignKey(d => d.Developerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Games__developer__59063A47");

            entity.HasMany(d => d.Geners).WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GenerGame",
                    r => r.HasOne<Gener>().WithMany()
                        .HasForeignKey("Generid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenerGame__gener__6477ECF3"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("Gameid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenerGame__gamei__6383C8BA"),
                    j =>
                    {
                        j.HasKey("Gameid", "Generid").HasName("PK__GenerGam__A2F01EDBCDA914F4");
                        j.ToTable("GenerGame");
                        j.IndexerProperty<Guid>("Gameid").HasColumnName("gameid");
                        j.IndexerProperty<Guid>("Generid").HasColumnName("generid");
                    });
        });

        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameSess__3213E83FAE5057AC");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");

            entity.HasOne(d => d.Game).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameSessi__gamei__5DCAEF64");

            entity.HasOne(d => d.Usuario).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.Usuarioid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameSessi__usuar__5CD6CB2B");
        });

        modelBuilder.Entity<Gener>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Geners__3213E83F1CF00DD9");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Offerid).HasName("PK__Offers__58871E98E362498C");

            entity.Property(e => e.Offerid).HasColumnName("offerid");
            entity.Property(e => e.Discountpct)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discountpct");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Startdate).HasColumnName("startdate");

            entity.HasOne(d => d.Game).WithMany(p => p.Offers)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offers__gameid__0A9D95DB");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3213E83FEBE36B09");

            entity.HasIndex(e => new { e.Userid, e.Gameid }, "UQ_User_Game").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Game).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__gameid__7F2BE32F");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__userid__7E37BEF6");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReviewCo__3213E83FC7D7D706");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Reviewid).HasColumnName("reviewid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.Reviewid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewCom__revie__02FC7413");

            entity.HasOne(d => d.User).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewCom__useri__03F0984C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F5655EC9E");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616471534E91").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC57227F8A18C").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Lastlogin)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("lastlogin");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(255)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasMany(d => d.Games).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Wishlist",
                    r => r.HasOne<Game>().WithMany()
                        .HasForeignKey("Gameid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Wishlist__gameid__07C12930"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Wishlist__userid__06CD04F7"),
                    j =>
                    {
                        j.HasKey("Userid", "Gameid").HasName("PK__Wishlist__760889D01CE24CE6");
                        j.ToTable("Wishlist");
                        j.IndexerProperty<Guid>("Userid").HasColumnName("userid");
                        j.IndexerProperty<Guid>("Gameid").HasColumnName("gameid");
                    });
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Achievementid }).HasName("PK__UserAchi__3244E2D4D013C7FF");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Achievementid).HasColumnName("achievementid");
            entity.Property(e => e.Unlockedat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("unlockedat");

            entity.HasOne(d => d.Achievement).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.Achievementid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAchie__achie__778AC167");

            entity.HasOne(d => d.User).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAchie__useri__76969D2E");
        });

        modelBuilder.Entity<UserGame>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Gameid }).HasName("PK__UserGame__760889D06BB17686");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Playtimehours)
                .HasDefaultValue(0)
                .HasColumnName("playtimehours");
            entity.Property(e => e.Purchasedate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("purchasedate");

            entity.HasOne(d => d.Game).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserGames__gamei__6A30C649");

            entity.HasOne(d => d.User).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserGames__useri__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
