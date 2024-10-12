using aspdotnet_project.App.Bill.entities;
using aspdotnet_project.App.Cinema.Entities;
using aspdotnet_project.App.Movie.Entities;
using aspdotnet_project.App.Show.Entities;
using aspdotnet_project.App.User;
using aspdotnet_project.App.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace aspdotnet_project.Context;

public class MyDbContext : IdentityDbContext<User>
{
    
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Bill> Bills { get; set; }
    public DbSet<BillStatus> BillStatus { get; set; }
    
    public DbSet<Location> Locations { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<CinemaStatus> CinemaStatus { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<HallStatus> HallStatus { get; set; }
    public DbSet<Seat> Seats { get; set; }

    public DbSet<Format> Formats { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieStatus> MovieStatus { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Movie?> Movies { get; set; }

    public DbSet<Show> Shows { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Create relationship in Bill and User

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Status)
            .WithMany(s => s.Bills)
            .HasForeignKey("status_id");

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bills)
            .HasForeignKey("user_id");
        
        //Create relationship in Cinema
        
        modelBuilder.Entity<Cinema>()
            .HasOne(c => c.Location)
            .WithMany(l => l.Cinemas)
            .HasForeignKey("location_id");

        modelBuilder.Entity<Cinema>()
            .HasOne(c => c.Status)
            .WithMany(cs => cs.Cinemas)
            .HasForeignKey("status_id");

        modelBuilder.Entity<Hall>()
            .HasOne(h => h.Status)
            .WithMany(hs => hs.Halls)
            .HasForeignKey("status_id");

        modelBuilder.Entity<Hall>()
            .HasOne(h => h.Cinema)
            .WithMany(c => c.Halls)
            .HasForeignKey("cinema_id");

        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Hall)
            .WithMany(h => h.Seats)
            .HasForeignKey("hall_id");
        
        //Create relationship in Movie

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Status)
            .WithMany(ms => ms.Movies)
            .HasForeignKey("status_id");

        modelBuilder.Entity<Image>()
            .HasOne(i => i.Movie)
            .WithMany(m => m.Images)
            .HasForeignKey("movie_id");

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Formats)
            .WithMany(f => f.Movies)
            .UsingEntity(j => j.ToTable("movie_format"));

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity(j => j.ToTable("movie_genre"));
        
        //Create relationship in Show

        modelBuilder.Entity<Show>()
            .HasOne(s => s.Movie)
            .WithMany(m => m.Shows)
            .HasForeignKey("movie_id");
        
        modelBuilder.Entity<Show>()
            .HasOne(s => s.Format)
            .WithMany(f => f.Shows)
            .HasForeignKey("format_id");
        
        modelBuilder.Entity<Show>()
            .HasOne(s => s.Hall)
            .WithMany(h => h.Shows)
            .HasForeignKey("hall_id");

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Show)
            .WithMany(s => s.Tickets)
            .HasForeignKey("show_id");
        
        //Create relationship in Ticket

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Bill)
            .WithMany(b => b.Tickets)
            .HasForeignKey("bill_id");
        
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Seat)
            .WithMany(s => s.Tickets)
            .HasForeignKey("seat_id");
        
        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },

            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        ];
        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}
