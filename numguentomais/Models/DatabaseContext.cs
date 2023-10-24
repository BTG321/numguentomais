using beyondthegame.Controllers;
using Microsoft.EntityFrameworkCore;

namespace beyondthegame.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<biblioteca> biblioteca { get; set; }
        public DbSet<usuario> usuario { get; set; }
        public DbSet<jogo> jogo { get; set; }
        public DbSet<empresa> empresa { get; set; }
        public DbSet<seguidores> seguidores { get; set; } // Adicione esta linha

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de relacionamento existentes

            modelBuilder.Entity<jogo>()
    .HasKey(j => j.id);
            

            // Configuração de relacionamento para a nova entidade 'seguidores'
            modelBuilder.Entity<seguidores>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.usuario_id);

            modelBuilder.Entity<seguidores>()
                .HasOne(s => s.empresa)
                .WithMany()
                .HasForeignKey(s => s.empresa_id);

            modelBuilder.Entity<jogo>()
                .HasOne(j => j.Empresa)
                .WithMany()
                .HasForeignKey(j => j.empresa_id);
        }
    }
}
