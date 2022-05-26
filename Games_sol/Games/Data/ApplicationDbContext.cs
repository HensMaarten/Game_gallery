using Games.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameSpelModus> GameSpelModussen { get; set; }
        public DbSet<GameTaal> GameTalen { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<LeeftijdCategorie> LeeftijdCategorieën { get; set; }
        public DbSet<Platform> Platformen { get; set; }
        public DbSet<SpelModus> Spelmodussen { get; set; }
        public DbSet<Taal> Talen { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Game>().ToTable("Game");
            builder.Entity<GameGenre>().ToTable("GameGenre");
            builder.Entity<GamePlatform>().ToTable("GamePlatform");
            builder.Entity<GameSpelModus>().ToTable("GameSpelModus");
            builder.Entity<GameTaal>().ToTable("GameTaal");
            builder.Entity<Genre>().ToTable("Genre");
            builder.Entity<LeeftijdCategorie>().ToTable("LeeftijdCategorie");
            builder.Entity<Platform>().ToTable("Platform");
            builder.Entity<SpelModus>().ToTable("SpelModus");
            builder.Entity<Taal>().ToTable("Taal");
            builder.Entity<GameUser>().ToTable("GameUser");
        }
    }
}
