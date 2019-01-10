using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanalReferenciaConfiguration : IEntityTypeConfiguration<EficaciaCanalReferencia> {

    public void Configure(EntityTypeBuilder<EficaciaCanalReferencia> builder){
        builder.ToTable("eficaciacanalreferencia").HasKey(ecr=> ecr.ID);                              
        builder.ToTable("eficaciacanalreferencia")
                .HasOne(ecr => ecr.EficaciaCanaisRelatorio)
                .WithOne(ecr=> ecr.Referencia);
    }
  }
}
                
