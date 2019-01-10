using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanaisRelatorioConfiguration : IEntityTypeConfiguration<EficaciaCanaisRelatorio> {

    public void Configure(EntityTypeBuilder<EficaciaCanaisRelatorio> builder){
        builder.ToTable("eficaciacanaisrelatorio").HasKey(ecr=> ecr.ID);
        builder.ToTable("eficaciacanaisrelatorio")
            .HasOne(ecr => ecr.Cliente)
            .WithMany(c => c.EficaciaCanalRelatorioLista)
            .HasForeignKey(ecr=> ecr.IdCliente);
    }
  }
}
                
