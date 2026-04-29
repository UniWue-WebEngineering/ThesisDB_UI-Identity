using Microsoft.EntityFrameworkCore;

namespace ThesisDB.Models
{
    public class ThesisDbContext : DbContext
    {
        public ThesisDbContext(DbContextOptions<ThesisDbContext> options) : base(options)
        {
        }

        public DbSet<Thesis> Theses { get; set; }
        public DbSet<Programme> Programmes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Thesis>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Thesis>()
                .Property(t => t.Type)
                .HasConversion<string>();

            // Konfiguration der 1:n-Beziehung zwischen Programme und Thesis
            modelBuilder.Entity<Thesis>()
                .HasOne(t => t.Programme)
                .WithMany(p => p.Theses)
                .HasForeignKey(t => t.ProgrammeId)
                .OnDelete(DeleteBehavior.Restrict); // Verhindert das Löschen eines Studiengangs, wenn noch Arbeiten zugeordnet sind
        }
    }
}
