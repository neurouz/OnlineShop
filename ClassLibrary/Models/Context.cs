using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Models
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public Context() { }
        public DbSet<Proizvod> proizvodi { get; set; }
        public DbSet<Uvoznik> uvoznici { get; set; }
        public DbSet<Kategorija> kategorije { get; set; }
        public DbSet<Recenzija> recenzije { get; set; }
        public DbSet<Oglas> oglasi { get; set; }
        public DbSet<KorisnikProizvod> KorisnikProizvod { get; set; }
        public DbSet<KorisnikOglas> KorisnikOglas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Oglas>().Property(oglas => oglas.Trajanje)
                .HasDefaultValue(0);

            base.OnModelCreating(modelBuilder);
        }
    }
}