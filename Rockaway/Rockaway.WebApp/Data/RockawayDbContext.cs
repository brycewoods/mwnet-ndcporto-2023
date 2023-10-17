namespace Rockaway.WebApp.Data;

using System.Linq;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sample;

public class RockawayDbContext : IdentityDbContext<IdentityUser> {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	public DbSet<Artist> Artists { get; set; } = default!;
	public DbSet<Venue> Venue { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		// Override EF Core's default table naming (which pluralizes entity names)
		// and use the same names as the C# classes instead
		var rockawayEntities = modelBuilder.Model.GetEntityTypes()
			.Where(e => e.ClrType.Namespace == typeof(Artist).Namespace);

		foreach (var entity in rockawayEntities) {
			entity.SetTableName(entity.DisplayName());
		}
		modelBuilder.Entity<Artist>().HasIndex(artist => artist.Slug).IsUnique();
		modelBuilder.Entity<Venue>().HasIndex(venue => venue.Slug).IsUnique();

		modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
		modelBuilder.Entity<Venue>().HasData(SampleData.Venues.AllVenues);
		modelBuilder.Entity<IdentityUser>().HasData(SampleData.Users.Admin);
	}
}