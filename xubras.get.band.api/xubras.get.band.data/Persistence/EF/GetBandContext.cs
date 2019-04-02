namespace xubras.get.band.data.Persistence.EF
{
    using Microsoft.EntityFrameworkCore;
    using xubras.get.band.data.Entities;

    public class GetBandContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        // public DbSet<Band> Bands { get; set; }

        public GetBandContext(DbContextOptions<GetBandContext> options)
               : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasKey(c => new { c.IdUser });
        }
    }
}