using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data.Entities;
namespace Rockaway.WebApp.Data;

using Entities;
using Microsoft.EntityFrameworkCore;
using Sample;

public class RockawayDbContext : DbContext {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	public DbSet<Artist> Artists { get; set; } = default!;
	public DbSet<Venue> Venue { get; set; } = default!;


	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
		modelBuilder.Entity<Venue>().HasData(SampleData.Venues.AllVenues);
	}

}