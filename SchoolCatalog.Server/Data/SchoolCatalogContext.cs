using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Data
{
    public class SchoolCatalogContext : DbContext
    {
        public SchoolCatalogContext(DbContextOptions<SchoolCatalogContext> options)
            : base(options)
        {
        }

        public DbSet<Elev> Elevi { get; set; }
        public DbSet<Profesor> Profesori { get; set; }
        public DbSet<Clasa> Clase { get; set; }
        public DbSet<Materie> Materii { get; set; }
        public DbSet<Nota> Note { get; set; }
        public DbSet<Tema> Teme { get; set; }
        public DbSet<FisierTema> FisiereTema { get; set; }
        public DbSet<Orar> Orare { get; set; }
        public DbSet<OrarItem> OrarItems { get; set; }
        public DbSet<Media> Medii { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clasa>()
                .HasOne(c => c.Orar)
                .WithOne(o => o.Clasa)
                .HasForeignKey<Orar>(o => o.IdClasa);

            modelBuilder.Entity<Clasa>()
                .HasMany(c => c.Elevi)
                .WithOne(e => e.Clasa)
                .HasForeignKey(e => e.ClasaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Clasa>()
                .HasMany(c => c.Materii)
                .WithMany(m => m.Clase);

            modelBuilder.Entity<Clasa>()
                .HasMany(c => c.Profesori)
                .WithMany(p => p.Clase);

            modelBuilder.Entity<Materie>()
                .HasOne(m => m.Profesor)
                .WithMany(p => p.Materii)
                .HasForeignKey(m => m.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tema>()
                .HasMany(t => t.Fisiere)
                .WithOne(f => f.Tema)
                .HasForeignKey(f => f.TemaId);

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Elev)
                .WithMany(e => e.Note)
                .HasForeignKey(n => n.IdElev);

            modelBuilder.Entity<Tema>()
                .HasOne(t => t.Clasa)
                .WithMany(e => e.Teme)
                .HasForeignKey(t => t.IdClasa)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Elev)
                .WithMany(e => e.Medii)
                .HasForeignKey(m => m.IdElev);

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Materie)
                .WithMany(m => m.Note)
                .HasForeignKey(n => n.IdMaterie);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Materie)
                .WithMany(mat => mat.Medii)
                .HasForeignKey(m => m.IdMaterie);

            modelBuilder.Entity<Orar>()
                .HasMany(o => o.OrarItems)
                .WithOne(oi => oi.Orar)
                .HasForeignKey(oi => oi.IdOrar);
        }
    }
}
