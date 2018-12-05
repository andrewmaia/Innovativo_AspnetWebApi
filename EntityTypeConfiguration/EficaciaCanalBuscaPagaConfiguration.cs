using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanalBuscaPagaConfiguration : IEntityTypeConfiguration<EficaciaCanalBuscaPaga> {

    public void Configure(EntityTypeBuilder<EficaciaCanalBuscaPaga> builder){
        builder.ToTable("EficaciaCanalBuscaPaga").HasKey(ecbp=> ecbp.ID);      
        builder.ToTable("EficaciaCanalBuscaPaga")
                .HasOne(ecbp => ecbp.EficaciaCanalRelatorio)
                .WithOne(ecr=> ecr.BuscaPaga);
    }
  }
}
