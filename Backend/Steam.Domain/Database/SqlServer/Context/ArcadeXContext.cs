using Microsoft.EntityFrameworkCore;
using Steam.Domain.Database.SqlServer.Entities;

namespace Steam.Domain.Database.SqlServer.Context;

public partial class ArcadeXContext : DbContext
{
    public ArcadeXContext()
    {
    }

    public ArcadeXContext(DbContextOptions<ArcadeXContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameSession> GameSessions { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserGame> UserGames { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;User=sa;Password=Admin1234@;DataBase=ArcadeX;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Achievem__3213E83FDC5B2147");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Game).WithMany(p => p.Achievements)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Achieveme__gamei__656C112C");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Friendid }).HasName("PK__Friends__2D04746573826B07");

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
                .HasConstraintName("FK__Friends__friendi__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.FriendUsers)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__userid__60A75C0F");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3213E83F2B956EF6");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Ownerid).HasColumnName("ownerid");
            entity.Property(e => e.Price)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Releasedate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("releasedate");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Owner).WithMany(p => p.Games)
                .HasForeignKey(d => d.Ownerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Games__deleteat__48CFD27E");

            entity.HasMany(d => d.Genres).WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GameGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("Genreid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GameGenre__genre__5629CD9C"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("Gameid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GameGenre__gamei__5535A963"),
                    j =>
                    {
                        j.HasKey("Gameid", "Genreid").HasName("PK__GameGenr__6956AEDE5D66EF54");
                        j.ToTable("GameGenres");
                        j.IndexerProperty<Guid>("Gameid").HasColumnName("gameid");
                        j.IndexerProperty<Guid>("Genreid").HasColumnName("genreid");
                    });
        });

        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameSess__3213E83FBEE8BDBE");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Game).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameSessi__gamei__4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameSessi__useri__4D94879B");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genres__3213E83F7EC0AA9B");

            entity.HasIndex(e => e.Name, "UQ__Genres__72E12F1B58514BD1").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Offerid).HasName("PK__Offers__58871E98B0D205DC");

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
                .HasConstraintName("FK__Offers__gameid__7F2BE32F");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3213E83F2C3A7C57");

            entity.HasIndex(e => new { e.Userid, e.Gameid }, "UQ_User_Game").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Game).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__gameid__71D1E811");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__userid__70DDC3D8");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReviewCo__3213E83FA8052261");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");
            entity.Property(e => e.Reviewid).HasColumnName("reviewid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.Reviewid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewCom__revie__75A278F5");

            entity.HasOne(d => d.User).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewCom__useri__76969D2E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F33A7FBF0");

            entity.HasIndex(e => e.Name, "UQ__Roles__72E12F1BDA48B634").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FA208CD17");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164F5297544").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572DB6422C4").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("createdat");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");
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
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Achievementid }).HasName("PK__UserAchi__3244E2D45A9267C4");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Achievementid).HasColumnName("achievementid");
            entity.Property(e => e.Unlockedat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("unlockedat");

            entity.HasOne(d => d.Achievement).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.Achievementid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAchie__achie__6A30C649");

            entity.HasOne(d => d.User).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAchie__useri__693CA210");
        });

        modelBuilder.Entity<UserGame>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Gameid }).HasName("PK__UserGame__760889D081A758F5");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Playtimeminutes)
                .HasDefaultValue(0)
                .HasColumnName("playtimeminutes");
            entity.Property(e => e.Purchasedate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("purchasedate");

            entity.HasOne(d => d.Game).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserGames__gamei__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.UserGames)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserGames__useri__5AEE82B9");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Roleid }).HasName("PK__UserRole__F77826E827680746");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__rolei__4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__useri__412EB0B6");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Gameid }).HasName("PK__Wishlist__760889D0BE097900");

            entity.ToTable("Wishlist");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Gameid).HasColumnName("gameid");
            entity.Property(e => e.Addedat)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("addedat");
            entity.Property(e => e.Deleteat).HasColumnName("deleteat");

            entity.HasOne(d => d.Game).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.Gameid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wishlist__gameid__7B5B524B");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wishlist__userid__7A672E12");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
