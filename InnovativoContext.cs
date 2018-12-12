using Microsoft.EntityFrameworkCore;
using  Innovativo.Models;
using Innovativo.EntityTypeConfiguration;

namespace Innovativo
{
    public class InnovativoContext : DbContext
    {
        public InnovativoContext(DbContextOptions<InnovativoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new EficaciaCanaisRelatorioConfiguration());
            modelBuilder.ApplyConfiguration(new EficaciaCanalBuscaPagaConfiguration());
            modelBuilder.ApplyConfiguration(new EficaciaCanalDiretoConfiguration());            
            modelBuilder.ApplyConfiguration(new EficaciaCanalDiretoConfiguration());                        
            modelBuilder.ApplyConfiguration(new EficaciaCanalEmailConfiguration());                                    
            modelBuilder.ApplyConfiguration(new EficaciaCanalOrganicoConfiguration());                      
        }        

        public DbSet<Cliente> Cliente { get; set; }        
        public DbSet<Usuario> Usuario { get; set; }                
        public DbSet<EficaciaCanaisRelatorio> EficaciaCanaisRelatorio { get; set; }
        public DbSet<EficaciaCanalBuscaPaga> EficaciaCanalBuscaPaga { get; set; }   
        public DbSet<EficaciaCanalDireto> EficaciaCanalDireto { get; set; }
        public DbSet<EficaciaCanalEmail> EficaciaCanalEmail { get; set; }        
        public DbSet<EficaciaCanalOrganico> EficaciaCanalOrganico { get; set; }                
        public DbSet<EficaciaCanalReferencia> EficaciaCanalReferencia { get; set; }        
    }        
}