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

        public MyDbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<DetailAccount> DetailAccounts { get; set; }
        public virtual DbSet<DetailGame> DetailGames { get; set; }
        public virtual DbSet<DetailRound> DetailRounds { get; set; }
        public virtual DbSet<DetailTeam> DetailTeams { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GamePlay> GamePlays { get; set; }
        public virtual DbSet<GameType> GameTypes { get; set; }
        public virtual DbSet<Localtion> Localtions { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-46U8TGP\\MSSQLSERVERCUONG;Initial Catalog=RunningMan;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.PassWord).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<DetailAccount>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.DetailAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetailAccount_Account");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.DetailAccounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetailAccount_AccountType");
            });

            modelBuilder.Entity<DetailGame>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.GameTypeId })
                    .HasName("pk_DetailGame");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.DetailGames)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetailGame_Game");

                entity.HasOne(d => d.GameType)
                    .WithMany(p => p.DetailGames)
                    .HasForeignKey(d => d.GameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetailGame_GameType");
            });

            modelBuilder.Entity<DetailRound>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.DetailRounds)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("fk_DetailRound_Game");

                entity.HasOne(d => d.Round)
                    .WithMany(p => p.DetailRounds)
                    .HasForeignKey(d => d.RoundId)
                    .HasConstraintName("fk_DetailRound_Round");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.DetailRounds)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("fk_DetailRound_Team");
            });

            modelBuilder.Entity<DetailTeam>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.DetailTeams)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetailTeam_Account");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.DetailTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DetailTeam_Team");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Level).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_Game_Account");
            });

            modelBuilder.Entity<GamePlay>(entity =>
            {
                entity.Property(e => e.RankGame).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamePlays)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("fk_GamePlay_Game");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.GamePlays)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("fk_GamePlay_Team");
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

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.RankTeam).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
