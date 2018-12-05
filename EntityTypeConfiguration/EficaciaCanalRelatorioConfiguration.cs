using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanalRelatorioConfiguration : IEntityTypeConfiguration<EficaciaCanalRelatorio> {

    public void Configure(EntityTypeBuilder<EficaciaCanalRelatorio> builder){
        builder.ToTable("EficaciaCanal").HasKey(ecr=> ecr.ID);
        builder.ToTable("EficaciaCanal")
            .HasOne(ecr => ecr.Cliente)
            .WithMany(c => c.EficaciaCanalRelatorioLista)
            .HasForeignKey(ecr=> ecr.IdCliente);
    }
  }
}
                
