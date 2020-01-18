using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public Context() { }
        public DbSet<Proizvod> proizvodi { get; set; }
        public DbSet<Uvoznik> uvoznici { get; set; }
        public DbSet<Kategorija> kategorije { get; set; }
        public DbSet<Recenzija> recenzije { get; set; }
        public DbSet<Oglas> oglasi { get; set; }
        public DbSet<AppUser> korisnici { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Oglas>().Property(oglas => oglas.Trajanje)
                .HasDefaultValue(0);
        }
    }
}