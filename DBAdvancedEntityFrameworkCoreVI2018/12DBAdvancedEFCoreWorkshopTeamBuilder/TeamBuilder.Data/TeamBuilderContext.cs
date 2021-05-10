using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.Data.EntityConfigurations;
using TeamBuilder.Models;

namespace TeamBuilder.Data
{
    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
        {

        }

        public TeamBuilderContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Invitation> Invitations { get; set; }

        public virtual DbSet<TeamEvent> EventTeams { get; set; }

        public virtual DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new UserTeamConfiguration());
            modelBuilder.ApplyConfiguration(new TeamEventConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}