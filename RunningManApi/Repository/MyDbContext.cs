using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RunningManApi.Repository.Entites;

#nullable disable

namespace RunningManApi.Repository
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameDetail> GameDetails { get; set; }
        public virtual DbSet<GamePlay> GamePlays { get; set; }
        public virtual DbSet<GameType> GameTypes { get; set; }
        public virtual DbSet<Localtion> Localtions { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionDetail> PermissionDetails { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesDetail> RolesDetails { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<RoundDetail> RoundDetails { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamDetail> TeamDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-46U8TGP;Initial Catalog=RunningMan;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Level).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_Game_Account");
            });

            modelBuilder.Entity<GameDetail>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.GameTypeId })
                    .HasName("pk_GameDetail");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameDetails)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_GameDetail_Game");

                entity.HasOne(d => d.GameType)
                    .WithMany(p => p.GameDetails)
                    .HasForeignKey(d => d.GameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_GameDetail_GameType");
            });

            modelBuilder.Entity<GamePlay>(entity =>
            {
                entity.Property(e => e.Rank).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamePlays)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("fk_GamePlay_Game");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.GamePlays)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("fk_GamePlay_Team");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.PermissionCode).IsUnicode(false);
            });

            modelBuilder.Entity<PermissionDetail>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.PermissionDetails)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PermissionDetail_Account");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionDetails)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PermissionDetail_Permission");
            });

            modelBuilder.Entity<Point>(entity =>
            {
                entity.Property(e => e.Point1).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Points)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_Point_Account");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Points)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("fk_Point_Team");
            });

            modelBuilder.Entity<RolesDetail>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.RolesDetails)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_RolesDetail_Account");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.RolesDetails)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_RolesDetail_Roles");
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Rounds)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Round_Account");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Rounds)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Round_Location");
            });

            modelBuilder.Entity<RoundDetail>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.RoundDetails)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("fk_RoundDetail_Game");

                entity.HasOne(d => d.Round)
                    .WithMany(p => p.RoundDetails)
                    .HasForeignKey(d => d.RoundId)
                    .HasConstraintName("fk_RoundDetail_Round");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.RoundDetails)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("fk_RoundDetail_Team");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Rank).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TeamDetail>(entity =>
            {
                entity.Property(e => e.TeamLead).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TeamDetails)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TeamDetail_Account");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamDetails)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TeamDetail_Team");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
