using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanalReferenciaConfiguration : IEntityTypeConfiguration<EficaciaCanalReferencia> {

    public void Configure(EntityTypeBuilder<EficaciaCanalReferencia> builder){
        builder.ToTable("EficaciaCanalReferencia").HasKey(ecr=> ecr.ID);                              
        builder.ToTable("EficaciaCanalReferencia")
                .HasOne(ecr => ecr.EficaciaCanaisRelatorio)
                .WithOne(ecr=> ecr.Referencia);
    }
  }
}
                
